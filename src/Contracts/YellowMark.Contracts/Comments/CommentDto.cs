using YellowMark.Contracts.Ads;
using YellowMark.Contracts.Users;

namespace YellowMark.Contracts.Comments;

/// <summary>
/// Comment Data Transfer Object.
/// </summary>
public class CommentDto
{
    /// <summary>
    /// Comment record identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Comment text.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Comment creation date.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Comment update date.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Comment author first name (<see cref="UserDto"/>).
    /// </summary>
    public String AuthorFirstName { get; set; }

    /// <summary>
    /// Comment author last name (<see cref="UserDto"/>).
    /// </summary>
    public String AuthorLastName { get; set; }
    
    /// <summary>
    /// Comment author (<see cref="UserDto"/>) Id.
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Commented ad Id (<see cref="AdDto"/>).
    /// </summary>
    public Guid AdId { get; set; }

    /// <summary>
    /// Ad title (<see cref="AdDto"/>).
    /// </summary>
    public string AdTitle { get; set; }
}