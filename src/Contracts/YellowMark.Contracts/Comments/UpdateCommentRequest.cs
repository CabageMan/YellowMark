namespace YellowMark.Contracts.Comments;

/// <summary>
/// Request data model for Comment update.
/// </summary>
public class UpdateCommentRequest
{
    /// <summary>
    /// Comment to update Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Comment text.
    /// </summary>
    public string Text { get; set; }
}