using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Comments.Repositories;
using YellowMark.Contracts.Comments;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.Comment.Repository;

/// <inheritdoc />
public class CommentRepository : ICommentRepository
{
    private readonly IWriteOnlyRepository<Domain.Comments.Entity.Comment> _writeOnlyrepository;
    private readonly IReadOnlyRepository<Domain.Comments.Entity.Comment> _readOnlyrepository;

    /// <summary>
    /// Init CommentRepository (<see cref="ICommentRepository"/>) instance.
    /// </summary>
    /// <param name="writeOnlyRepository"><see cref="IWriteOnlyRepository"/></param>
    /// <param name="readOnlyRepository"><see cref="IReadOnlyRepository"/></param>
    public CommentRepository(
        IWriteOnlyRepository<Domain.Comments.Entity.Comment> writeOnlyRepository,
        IReadOnlyRepository<Domain.Comments.Entity.Comment> readOnlyRepository)
    {
        _writeOnlyrepository = writeOnlyRepository;
        _readOnlyrepository = readOnlyRepository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CommentDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var comments = await _readOnlyrepository.GetAll().ToListAsync(cancellationToken);

        // TODO: Find a better way to get User and Ad info from other DB.
        return comments.Select(comment => new CommentDto
        {
            Id = comment.Id,
            Text = comment.Text,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            // AuthorFirstName = ,
            // AuthorLastName = ,
            AuthorId = comment.UserId,
            AdId = comment.AdId,
            // AdTitle =
        });
    }
}
