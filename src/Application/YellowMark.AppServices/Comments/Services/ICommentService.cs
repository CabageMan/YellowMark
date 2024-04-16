using YellowMark.Contracts;
using YellowMark.Contracts.Comments;
using YellowMark.Contracts.Pagination;

namespace YellowMark.AppServices.Comments.Services;

/// <summary>
/// Comments service.
/// </summary>
public interface ICommentService
{
    /// <summary>
    /// Create new Comment instance from the request params. 
    /// </summary>
    /// <param name="request">Comment request model <see cref="CreateCommentRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Created Comment id <see cref="Guid"/></returns>
    Task<Guid> AddCommentAsync(CreateCommentRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all comments with pagination.
    /// </summary>
    /// <param name="request">Pagination params <see cref="GetAllRequestWithPagination"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Result with pagination params <see cref="ResultWithPagination"/>.</returns>
    Task<ResultWithPagination<CommentDto>> GetCommentsAsync(GetAllRequestWithPagination request, CancellationToken cancellationToken);

    /// <summary>
    /// Returns an instance of the <see cref="CommentDto"/> by id.
    /// </summary>
    /// <param name="id">Comments id</param>
    /// <returns><see cref="CommentDto"/></returns>
    Task<CommentDto> GetCommentByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all Comments matched the text.
    /// </summary>
    /// <param name="searchRequestString">Find all comments mathes this string in texts and users names</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Comments collection of <see cref="CommentDto"/>.</returns>
    Task<IEnumerable<CommentDto>> GetCommentsByStringAsync(string searchRequestString, CancellationToken cancellationToken);

    /// <summary>
    /// Update current Comment.
    /// </summary>
    /// <param name="id">Comment id</param>
    /// <param name="request">Creation model <see cref="CreateCommentRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Updated ad <see cref="CommentDto"/></returns>
    Task<CommentDto> UpdateCommentAsync(Guid id, CreateCommentRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Delete the Comment by id.
    /// </summary>
    /// <param name="id">Comment id <see cref="Guid"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteCommentByIdAsync(Guid id, CancellationToken cancellationToken);
}
