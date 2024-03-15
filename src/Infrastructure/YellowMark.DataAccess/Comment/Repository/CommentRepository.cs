using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using YellowMark.AppServices.Comments.Repositories;
using YellowMark.Contracts.Comments;
using YellowMark.Infrastructure.Repository;

namespace YellowMark.DataAccess.Comment.Repository;

/// <inheritdoc />
public class CommentRepository : ICommentRepository
{
    /*
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
    */

    /// <inheritdoc />
    public async Task<IEnumerable<CommentDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var comments = MockList();
        return await Task.Run(() => comments.Select(comment => new CommentDto
        {
            Id = comment.Id,
            Text = comment.Text,
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt,
            AuthorFirstName = "Blob",
            AuthorLastName = "Awesome",
            AuthorId = comment.UserId,
            AdId = comment.AdId,
            AdTitle = "Mock Ad"
        }));
        /*
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
        */
    }

    // Mock Mock Data
    private static List<Domain.Comments.Entity.Comment> MockList()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                Text = "First Blob's comment",
                UserId = Guid.NewGuid(),
                AdId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "Second Blob's comment",
                UserId = Guid.NewGuid(),
                AdId = Guid.NewGuid()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "Third Blob's comment",
                UserId = Guid.NewGuid(),
                AdId = Guid.NewGuid()
            },
        ];
    }
}
