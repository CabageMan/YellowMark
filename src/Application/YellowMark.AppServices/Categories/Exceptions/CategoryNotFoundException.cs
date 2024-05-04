using System.Diagnostics.CodeAnalysis;

namespace YellowMark.AppServices.Categories.Exceptions;

/// <summary>
/// Category Not found exception.
/// </summary>
public class CategoryNotFoundException : Exception
{
    /// <summary>
    /// <see cref="CategoryNotFoundException"/> constructor.
    /// </summary>
    public CategoryNotFoundException()
    { }

    /// <summary>
    /// <see cref="CategoryNotFoundException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public CategoryNotFoundException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="CategoryNotFoundException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public CategoryNotFoundException(string message, Exception inner) : base(message, inner)
    { }

    /// <summary>
    /// Throw <see cref="CategoryNotFoundException"/> if argument is null.
    /// </summary>
    /// <param name="argument">Any optional object.</param>
    /// <param name="message">Exception message.</param>
    /// <exception cref="CategoryNotFoundException"></exception>
    public static void ThrowIfNull([NotNull] object? argument, string? message = null)
    {
        if (argument == null)
        {
            throw message == null ? new CategoryNotFoundException() : new CategoryNotFoundException(message);
        }
    }
}
