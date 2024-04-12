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
    public bool IsEdited { get; set; }

    /// <summary>
    /// Comment author Id.
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Comment author first name.
    /// </summary>
    public String AuthorName { get; set; }

    /// <summary>
    /// Comment author last name.
    /// </summary>
    public String AuthorLastName { get; set; }

    /// <summary>
    /// Commented ad Id.
    /// </summary>
    public Guid AdId { get; set; }

    /// <summary>
    /// Ad title.
    /// </summary>
    public string AdTitle { get; set; }
}