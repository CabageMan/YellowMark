using AutoMapper;
using YellowMark.AppServices.Categories.Repositories;
using YellowMark.Contracts.Categories;
using YellowMark.Domain.Categories.Entity;

namespace YellowMark.AppServices.Categories.Services;

/// <inheritdoc cref="ICategoryService"/>
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init <see cref="CategoryService"/> instance.
    /// </summary>
    /// <param name="categoryRepository">Category Repository (<see cref="ICategoryRepository"/>)</param>
    /// <param name="mapper">Category mapper</param>
    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddCategoryAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateCategoryRequest, Category>(request);
        await _categoryRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc/>
    public Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        return _categoryRepository.GetAllAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<CategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetByIdAsync(id, cancellationToken);
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
