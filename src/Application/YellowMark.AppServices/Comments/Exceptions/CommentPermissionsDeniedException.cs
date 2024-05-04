namespace YellowMark.AppServices.Comments.Exceptions;

/// <summary>
/// Comment permissions denied exception.
/// </summary>
public class CommentPermissionsDeniedException : Exception {
    /// <summary>
    /// <see cref="CommentPermissionsDeniedException"/> constructor.
    /// </summary>
    public CommentPermissionsDeniedException()
    { }

    /// <summary>
    /// <see cref="CommentPermissionsDeniedException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public CommentPermissionsDeniedException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="CommentPermissionsDeniedException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public CommentPermissionsDeniedException(string message, Exception inner) : base(message, inner)
    { }
}
