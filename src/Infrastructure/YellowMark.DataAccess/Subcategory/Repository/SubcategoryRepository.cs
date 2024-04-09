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
    private readonly IWriteOnlyRepository<Domain.Subcategories.Entity.Subcategory, WriteDbContext> _writeOnlyrepository;
    private readonly IReadOnlyRepository<Domain.Subcategories.Entity.Subcategory, ReadDbContext> _readOnlyrepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init SubcategoryRepository (<see cref="ISubcategoryRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    public SubcategoryRepository(
        IWriteOnlyRepository<Domain.Subcategories.Entity.Subcategory, WriteDbContext> writeOnlyRepository,
        IReadOnlyRepository<Domain.Subcategories.Entity.Subcategory, ReadDbContext> readOnlyRepository,
        IMapper mapper)
    {
        _writeOnlyrepository = writeOnlyRepository;
        _readOnlyrepository = readOnlyRepository;
    }

    /// <inheritdoc/>
    public async Task AddAsync(Domain.Subcategories.Entity.Subcategory entity, CancellationToken cancellationToken)
    {
        await _writeOnlyrepository.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<SubcategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _readOnlyrepository
            .GetAll()
            .ProjectTo<SubcategoryDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<SubcategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _readOnlyrepository
            .GetAll()
            .Where(s => s.Id == id)
            .ProjectTo<SubcategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<SubcategoryDto>> GetFiltered(Specification<Domain.Subcategories.Entity.Subcategory> specification, CancellationToken cancellationToken)
    {
        return await _readOnlyrepository
            .GetAll()
            .Where(specification.ToExpression())
            .ProjectTo<SubcategoryDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Domain.Subcategories.Entity.Subcategory entity, CancellationToken cancellationToken)
    {
        await _writeOnlyrepository.UpdateAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _writeOnlyrepository.DeleteAsync(id, cancellationToken);
    }
}
