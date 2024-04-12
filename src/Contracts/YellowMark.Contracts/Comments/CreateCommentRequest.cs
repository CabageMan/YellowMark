namespace YellowMark.Contracts;

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
    /// Author's (User) Id.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Ad Id.
    /// </summary>
    public Guid AdId { get; set; }
}
