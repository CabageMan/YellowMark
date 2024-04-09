using AutoMapper;
using YellowMark.AppServices.Subcategories.Repositories;
using YellowMark.Contracts.Subcategories;
using YellowMark.Domain.Subcategories.Entity;

namespace YellowMark.AppServices.Subcategories.Services;

/// <inheritdoc cref="ISubcategoryService"/>
public class SubcategoryService : ISubcategoryService
{

    private readonly ISubcategoryRepository _subcategoryRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init <see cref="SubcategoryService"/> instance.
    /// </summary>
    /// <param name="subcategoryRepository">Subcategory Repository (<see cref="ISubcategoryRepository"/>)</param>
    /// <param name="mapper">Subcategory mapper</param>
    public SubcategoryService(ISubcategoryRepository subcategoryRepository, IMapper mapper)
    {
        _subcategoryRepository = subcategoryRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<Guid> AddSubcategoryAsync(CreateSubcategoryRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateSubcategoryRequest, Subcategory>(request);
        await _subcategoryRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc/>
    public Task<IEnumerable<SubcategoryDto>> GetSubcategoriesAsync(CancellationToken cancellationToken)
    {
        return _subcategoryRepository.GetAllAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SubcategoryDto> GetSubcategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _subcategoryRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SubcategoryDto> UpdateSubcategoryAsync(Guid id, CreateSubcategoryRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateSubcategoryRequest, Subcategory>(request);
        entity.Id = id;

        await  _subcategoryRepository.UpdateAsync(entity, cancellationToken);

        return _mapper.Map<Subcategory, SubcategoryDto>(entity);
    }

    /// <inheritdoc/>
    public async Task DeleteSubcategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _subcategoryRepository.DeleteAsync(id, cancellationToken);
    }
}
