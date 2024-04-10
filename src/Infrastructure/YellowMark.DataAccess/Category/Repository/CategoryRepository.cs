using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Categories.Repositories;
using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Categories;
using YellowMark.DataAccess.DatabaseContext;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.Category.Repository;

/// <inheritdoc cref="ICategoryRepository"/>
public class CategoryRepository : ICategoryRepository
{
    private readonly IWriteOnlyRepository<Domain.Categories.Entity.Category, WriteDbContext> _writeOnlyRepository;
    private readonly IReadOnlyRepository<Domain.Categories.Entity.Category, ReadDbContext> _readOnlyRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init CategoryRepository (<see cref="ICategoryRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    /// <param name="mapper"><see cref="IMapper"/></param>
    public CategoryRepository(
        IWriteOnlyRepository<Domain.Categories.Entity.Category, WriteDbContext> writeOnlyRepository,
        IReadOnlyRepository<Domain.Categories.Entity.Category, ReadDbContext> readOnlyRepository,
        IMapper mapper)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task AddAsync(Domain.Categories.Entity.Category entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _readOnlyRepository
            .GetAll()
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _readOnlyRepository
            .GetAll()
            .Where(s => s.Id == id)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CategoryDto>> GetFiltered(Specification<Domain.Categories.Entity.Category> specification, CancellationToken cancellationToken)
    {
        return await _readOnlyRepository
            .GetAll()
            .Where(specification.ToExpression())
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Domain.Categories.Entity.Category entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.UpdateAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.DeleteAsync(id, cancellationToken);
    }
}
