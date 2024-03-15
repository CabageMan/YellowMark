using YellowMark.Contracts.Comments;

namespace YellowMark.AppServices.Comments.Repositories;

/// <summary>
/// Comments repository.
/// </summary>
public interface ICommentRepository
{
    /// <summary>
    /// Returns all instances of the CommentDto. 
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CommentDto"/></returns>
    Task<IEnumerable<CommentDto>> GetAllAsync(CancellationToken cancellationToken);
}
