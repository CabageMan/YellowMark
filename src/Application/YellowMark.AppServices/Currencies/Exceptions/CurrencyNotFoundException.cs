using System.Diagnostics.CodeAnalysis;

namespace YellowMark.AppServices.Currencies.Exceptions;

/// <summary>
/// Currency operation exception.
/// </summary>
public class CurrencyNotFoundException : Exception
{
    /// <summary>
    /// <see cref="CurrencyNotFoundException"/> constructor.
    /// </summary>
    public CurrencyNotFoundException()
    { }

    /// <summary>
    /// <see cref="CurrencyNotFoundException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public CurrencyNotFoundException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="CurrencyNotFoundException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public CurrencyNotFoundException(string message, Exception inner) : base(message, inner)
    { }

    /// <summary>
    /// Throw <see cref="CurrencyNotFoundException"/> if argument is null.
    /// </summary>
    /// <param name="argument">Any optional object.</param>
    /// <param name="message">Exception message.</param>
    /// <exception cref="CurrencyNotFoundException"></exception>
    public static void ThrowIfNull([NotNull] object? argument, string? message = null)
    {
        if (argument == null)
        {
            throw message == null ? new CurrencyNotFoundException() : new CurrencyNotFoundException(message);
        }
    }
}
