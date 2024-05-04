using System.Diagnostics.CodeAnalysis;

namespace YellowMark.AppServices.Ads.Exceptions;

/// <summary>
/// Ad operation exception.
/// </summary>
public class AdOperationException : Exception {
    /// <summary>
    /// <see cref="AdOperationException"/> constructor.
    /// </summary>
    public AdOperationException()
    { }

    /// <summary>
    /// <see cref="AdOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public AdOperationException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="AdOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public AdOperationException(string message, Exception inner) : base(message, inner)
    { }

    /// <summary>
    /// Throw <see cref="AdOperationException"/> if argument is null.
    /// </summary>
    /// <param name="argument">Any optional object.</param>
    /// <param name="message">Exception message.</param>
    /// <exception cref="AdOperationException"></exception>
    public static void ThrowIfNull([NotNull] object? argument, string? message = null)
    {
        if (argument == null)
        {
            throw message == null ? new AdOperationException() : new AdOperationException(message);
        }
    }
}
