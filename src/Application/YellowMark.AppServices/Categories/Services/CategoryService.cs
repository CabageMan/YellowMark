﻿using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using YellowMark.AppServices.Categories.Repositories;
using YellowMark.Contracts.Categories;
using YellowMark.Domain.Categories.Entity;

namespace YellowMark.AppServices.Categories.Services;

/// <inheritdoc cref="ICategoryService"/>
// TODO: Rework Categories and SubCategories.
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _distributedCache;

    /// <summary>
    /// Init <see cref="CategoryService"/> instance.
    /// </summary>
    /// <param name="categoryRepository">Category Repository (<see cref="ICategoryRepository"/>)</param>
    /// <param name="mapper">Category mapper</param>
    /// <param name="memoryCache">Memory cache <see cref="IMemoryCache"/></param>
    /// <param name="distributedCache">Distributed cache <see cref="IDistributedCache"/></param>
    public CategoryService(
        ICategoryRepository categoryRepository, 
        IMapper mapper,
        IMemoryCache memoryCache,
        IDistributedCache distributedCache)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateCategoryRequest, Category>(request);
        await _categoryRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var cacheKey = "all_categories";

        var categoriesSerialized = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrWhiteSpace(categoriesSerialized))
        {
            var cachedCategories = JsonSerializer.Deserialize<IReadOnlyCollection<CategoryDto>>(categoriesSerialized);
            if (cachedCategories != null)
            {
                return cachedCategories;
            }
        }

        var categories = await _categoryRepository.GetAllAsync(cancellationToken);
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
    public async Task<CategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cacheKey = $"category_info_{id}";

        if (_memoryCache.TryGetValue<CategoryDto>(cacheKey, out var result) && result != null)
        {
            return result;
        }

        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        _memoryCache.Set(
            cacheKey, 
            category, 
            new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5) }
        );

        return category;
    }

    /// <inheritdoc/>
    public async Task<CategoryDto> UpdateCategoryAsync(Guid id, CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        // TODO: Need to fix. Get previous record and update it (Created at id wrong).
        var entity = _mapper.Map<CreateCategoryRequest, Category>(request);
        entity.Id = id;

        await  _categoryRepository.UpdateAsync(entity, cancellationToken);

        return _mapper.Map<Category, CategoryDto>(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteAsync(id, cancellationToken);
    }
}
