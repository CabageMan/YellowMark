using System.Diagnostics.CodeAnalysis;

namespace YellowMark.AppServices.Categories.Exceptions;

/// <summary>
/// Category operation exception.
/// </summary>
public class CategoryOperationException : Exception
{
    /// <summary>
    /// <see cref="CategoryOperationException"/> constructor.
    /// </summary>
    public CategoryOperationException()
    { }

    /// <summary>
    /// <see cref="CategoryOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public CategoryOperationException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="CategoryOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public CategoryOperationException(string message, Exception inner) : base(message, inner)
    { }

    /// <summary>
    /// Throw <see cref="CategoryOperationException"/> if argument is null.
    /// </summary>
    /// <param name="argument">Any optional object.</param>
    /// <param name="message">Exception message.</param>
    /// <exception cref="CategoryOperationException"></exception>
    public static void ThrowIfNull([NotNull] object? argument, string? message = null)
    {
        if (argument == null)
        {
            throw message == null ? new CategoryOperationException() : new CategoryOperationException(message);
        }
    }

    /// <summary>
    /// Throw <see cref="CategoryOperationException"/> if argument is FALSE.
    /// </summary>
    /// <param name="argument">Bool value.</param>
    /// <param name="message">Exception message.</param>
    /// <exception cref="CategoryOperationException"></exception>
    public static void ThrowIfFalse(bool argument, string? message = null)
    {
        if (!argument)
        {
            throw message == null ? new CategoryOperationException() : new CategoryOperationException(message);
        }
    }
}
