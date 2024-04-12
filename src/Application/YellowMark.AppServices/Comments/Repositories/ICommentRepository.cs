using YellowMark.AppServices.Specifications;
using YellowMark.Contracts.Comments;
using YellowMark.Contracts.Pagination;

namespace YellowMark.AppServices.Comments.Repositories;

/// <summary>
/// Comments repository.
/// </summary>
public interface ICommentRepository
{
    /// <summary>
    /// Add new Comment instance.
    /// </summary>
    /// <param name="entity">Comment model <see cref="Domain.Comments.Entity.Comment"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task AddAsync(Domain.Comments.Entity.Comment entity, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the CommentDto. 
    /// </summary>
    /// <param name="request"><see cref="GetAllRequestWithPagination"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Result with pagination params <see cref="ResultWithPagination"/> with <see cref="CommentDto"/></returns>
    Task<ResultWithPagination<CommentDto>> GetAllAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all instances of the <see cref="CommentDto"/> matched to specification.
    /// </summary>
    /// <param name="specification">Filtering specification <see cref="Specification"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Collection of the <see cref="CommentDto"/></returns>
    Task<IEnumerable<CommentDto>> GetFiltered(Specification<Domain.Comments.Entity.Comment> specification, CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="CommentDto"/> by id.
    /// </summary>
    /// <param name="id">Comment id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CommentDto"/></returns>
    Task<CommentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Update current Comment.
    /// </summary>
    /// <param name="entity">Entity model <see cref="Domain.Comments.Entity.Comment"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task UpdateAsync(Domain.Comments.Entity.Comment entity, CancellationToken cancellationToken);

    /// <summary>
    /// Delete a Comment by id.
    /// </summary>
    /// <param name="id">Comment id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
