using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Comments.Repositories;
using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Comments;
using YellowMark.Contracts.Pagination;
using YellowMark.DataAccess.DatabaseContext;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.Comment.Repository;

/// <inheritdoc cref="ICommentRepository"/>
public class CommentRepository : ICommentRepository
{
    private readonly IWriteOnlyRepository<Domain.Comments.Entity.Comment, WriteDbContext> _writeOnlyRepository;
    private readonly IReadOnlyRepository<Domain.Comments.Entity.Comment, ReadDbContext> _readOnlyRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Init CommentRepository (<see cref="ICommentRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    /// <param name="mapper"><see cref="IMapper"/></param>
    public CommentRepository(
        IWriteOnlyRepository<Domain.Comments.Entity.Comment, WriteDbContext> writeOnlyRepository,
        IReadOnlyRepository<Domain.Comments.Entity.Comment, ReadDbContext> readOnlyRepository,
        IMapper mapper)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task AddAsync(Domain.Comments.Entity.Comment entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<ResultWithPagination<CommentDto>> GetAllAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken)
    {
        var result = new ResultWithPagination<CommentDto>();

        var query = _readOnlyRepository.GetAll();

        var elementsCount = await query.CountAsync(cancellationToken);
        result.AvailablePages = (int)Math.Ceiling((double)(elementsCount / request.BatchSize));

        var pagesToSkip = request.BatchSize * Math.Max((request.PageNumber - 1), 0);
        var paginationQuery = await query
            .OrderBy(c => c.Id)
            .Skip(pagesToSkip)
            .Take(request.BatchSize)
            .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        result.Result = paginationQuery;

        return result;
    }

    /// <inheritdoc/>
    public async Task<Domain.Comments.Entity.Comment> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _readOnlyRepository
            .GetAll()
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CommentDto>> GetFiltered(Specification<Domain.Comments.Entity.Comment> specification, CancellationToken cancellationToken)
    {
        // TODO: Add pagination
        return await _readOnlyRepository
            .GetAll()
            .Where(specification.ToExpression())
            .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(Domain.Comments.Entity.Comment entity, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.UpdateAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _writeOnlyRepository.DeleteAsync(id, cancellationToken);
    }
}
