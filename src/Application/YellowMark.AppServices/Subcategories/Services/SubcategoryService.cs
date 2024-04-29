using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using YellowMark.AppServices.Subcategories.Repositories;
using YellowMark.Contracts.Subcategories;
using YellowMark.Domain.Subcategories.Entity;

namespace YellowMark.AppServices.Subcategories.Services;

/// <inheritdoc cref="ISubcategoryService"/>
public class SubcategoryService : ISubcategoryService
{
    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _distributedCache;

    /// <summary>
    /// Init <see cref="SubcategoryService"/> instance.
    /// </summary>
    /// <param name="subcategoryRepository">Subcategory Repository (<see cref="ISubcategoryRepository"/>)</param>
    /// <param name="mapper">Subcategory mapper</param>
    /// <param name="memoryCache">Memory cache <see cref="IMemoryCache"/></param>
    /// <param name="distributedCache">Distributed cache <see cref="IDistributedCache"/></param>
    public SubcategoryService(
        ISubcategoryRepository subcategoryRepository,
        IMapper mapper,
        IMemoryCache memoryCache,
        IDistributedCache distributedCache)
    {
        _subcategoryRepository = subcategoryRepository;
        _mapper = mapper;
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddSubcategoryAsync(CreateSubcategoryRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateSubcategoryRequest, Subcategory>(request);
        await _subcategoryRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<SubcategoryDto>> GetSubcategoriesAsync(CancellationToken cancellationToken)
    {
        var cacheKey = "all_subcategories";
        var categoriesSerialized = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrWhiteSpace(categoriesSerialized))
        {
            var cachedCategories = JsonSerializer.Deserialize<IReadOnlyCollection<SubcategoryDto>>(categoriesSerialized);
            if (cachedCategories != null)
            {
                return cachedCategories;
            }
        }

        var categories = await _subcategoryRepository.GetAllAsync(cancellationToken);
        categoriesSerialized = JsonSerializer.Serialize(categories);
        await _distributedCache.SetStringAsync(
            cacheKey,
            categoriesSerialized,
            new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5) },
            cancellationToken)
        ;

        return categories;
    }

    /// <inheritdoc/>
    public async Task<SubcategoryDto> GetSubcategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cacheKey = $"subcategory_info_{id}";

        if (_memoryCache.TryGetValue<Subcategory>(cacheKey, out var result) && result != null)
        {
            return _mapper.Map<SubcategoryDto>(result);
        }

        var category = await _subcategoryRepository.GetByIdAsync(id, cancellationToken);

        _memoryCache.Set(
            cacheKey,
            category,
            new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5) }
        );

        return _mapper.Map<SubcategoryDto>(category);
    }

    /// <inheritdoc/>
    public async Task<SubcategoryDto> UpdateSubcategoryAsync(Guid id, CreateSubcategoryRequest request, CancellationToken cancellationToken)
    {
        var currentEntity = await _subcategoryRepository.GetByIdAsync(id, cancellationToken);

        var updatedEntity = _mapper.Map<CreateSubcategoryRequest, Subcategory>(request);
        updatedEntity.Id = id;
        updatedEntity.CreatedAt = currentEntity.CreatedAt;

        await _subcategoryRepository.UpdateAsync(updatedEntity, cancellationToken);

        return _mapper.Map<Subcategory, SubcategoryDto>(updatedEntity);
    }

    /// <inheritdoc/>
    public async Task DeleteSubcategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _subcategoryRepository.DeleteAsync(id, cancellationToken);
    }
}
