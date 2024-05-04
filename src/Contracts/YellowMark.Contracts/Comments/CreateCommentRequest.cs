namespace YellowMark.Contracts.Comments;

/// <summary>
/// Request data model for Comment creation.
/// </summary>
public class CreateCommentRequest
{
    /// <summary>
    /// Comment text.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Ad Id.
    /// </summary>
    public Guid AdId { get; set; }
}
