using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Specifications;
using YellowMark.AppServices.Subcategories.Repositories;
using YellowMark.Contracts.Subcategories;
using YellowMark.DataAccess.DatabaseContext;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.Subcategory.Repository;

/// <inheritdoc cref="ISubcategoryRepository"/>
public class SubcategoryRepository : ISubcategoryRepository
{
    private readonly IWriteOnlyRepository<Domain.Subcategories.Entity.Subcategory, WriteDbContext> _writeOnlyRepository;
    private readonly IReadOnlyRepository<Domain.Subcategories.Entity.Subcategory, ReadDbContext> _readOnlyRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init SubcategoryRepository (<see cref="ISubcategoryRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    /// <param name="mapper"><see cref="IMapper"/></param>
    public SubcategoryRepository(
        IWriteOnlyRepository<Domain.Subcategories.Entity.Subcategory, WriteDbContext> writeOnlyRepository,
        IReadOnlyRepository<Domain.Subcategories.Entity.Subcategory, ReadDbContext> readOnlyRepository,
        IMapper mapper)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task AddAsync(Domain.Subcategories.Entity.Subcategory entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<SubcategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _readOnlyRepository
            .GetAll()
            .ProjectTo<SubcategoryDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SubcategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _readOnlyRepository
            .GetAll()
            .Where(s => s.Id == id)
            .ProjectTo<SubcategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<SubcategoryDto>> GetFiltered(Specification<Domain.Subcategories.Entity.Subcategory> specification, CancellationToken cancellationToken)
    {
        return await _readOnlyRepository
            .GetAll()
            .Where(specification.ToExpression())
            .ProjectTo<SubcategoryDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Domain.Subcategories.Entity.Subcategory entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.UpdateAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.DeleteAsync(id, cancellationToken);
    }
}
