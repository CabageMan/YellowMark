using System.Diagnostics.CodeAnalysis;

namespace YellowMark.AppServices.Comments.Exceptions;

/// <summary>
/// Comment Not found exception.
/// </summary>
public class CommentNotFoundException : Exception {
    /// <summary>
    /// <see cref="CommentNotFoundException"/> constructor.
    /// </summary>
    public CommentNotFoundException()
    { }

    /// <summary>
    /// <see cref="CommentNotFoundException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public CommentNotFoundException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="CommentNotFoundException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public CommentNotFoundException(string message, Exception inner) : base(message, inner)
    { }

    /// <summary>
    /// Throw <see cref="CommentNotFoundException"/> if argument is null.
    /// </summary>
    /// <param name="argument">Any optional object.</param>
    /// <param name="message">Exception message.</param>
    /// <exception cref="CommentNotFoundException"></exception>
    public static void ThrowIfNull([NotNull] object? argument, string? message = null)
    {
        if (argument == null)
        {
            throw message == null ? new CommentNotFoundException() : new CommentNotFoundException(message);
        }
    }
}
