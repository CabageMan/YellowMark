using System.Diagnostics.CodeAnalysis;

namespace YellowMark.AppServices.Comments.Exceptions;

/// <summary>
/// Comment operation exception.
/// </summary>
public class CommentOperationException : Exception {
    /// <summary>
    /// <see cref="CommentOperationException"/> constructor.
    /// </summary>
    public CommentOperationException()
    { }

    /// <summary>
    /// <see cref="CommentOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public CommentOperationException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="CommentOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public CommentOperationException(string message, Exception inner) : base(message, inner)
    { }

    /// <summary>
    /// Throw <see cref="CommentOperationException"/> if argument is null.
    /// </summary>
    /// <param name="argument">Any optional object.</param>
    /// <param name="message">Exception message.</param>
    /// <exception cref="CommentOperationException"></exception>
    public static void ThrowIfNull([NotNull] object? argument, string? message = null)
    {
        if (argument == null)
        {
            throw message == null ? new CommentOperationException() : new CommentOperationException(message);
        }
    }

    /// <summary>
    /// Throw <see cref="CommentOperationException"/> if argument is FALSE.
    /// </summary>
    /// <param name="argument">Bool value.</param>
    /// <param name="message">Exception message.</param>
    /// <exception cref="CommentOperationException"></exception>
    public static void ThrowIfFalse(bool argument, string? message = null)
    {
        if (!argument)
        {
            throw message == null ? new CommentOperationException() : new CommentOperationException(message);
        }
    }
}