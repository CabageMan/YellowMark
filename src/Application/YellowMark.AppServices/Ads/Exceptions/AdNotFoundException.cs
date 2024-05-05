using System.Diagnostics.CodeAnalysis;

namespace YellowMark.AppServices.Ads.Exceptions;

/// <summary>
/// Ad Not found exception.
/// </summary>
public class AdNotFoundException : Exception {
    /// <summary>
    /// <see cref="AdNotFoundException"/> constructor.
    /// </summary>
    public AdNotFoundException()
    { }

    /// <summary>
    /// <see cref="AdNotFoundException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public AdNotFoundException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="AdNotFoundException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public AdNotFoundException(string message, Exception inner) : base(message, inner)
    { }

    /// <summary>
    /// Throw <see cref="AdNotFoundException"/> if argument is null.
    /// </summary>
    /// <param name="argument">Any optional object.</param>
    /// <param name="message">Exception message.</param>
    /// <exception cref="AdNotFoundException"></exception>
    public static void ThrowIfNull([NotNull] object? argument, string? message = null)
    {
        if (argument == null)
        {
            throw message == null ? new AdNotFoundException() : new AdNotFoundException(message);
        }
    }
}

