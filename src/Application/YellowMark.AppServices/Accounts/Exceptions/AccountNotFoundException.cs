using System.Diagnostics.CodeAnalysis;

namespace YellowMark.AppServices.Accounts.Exceptions;

/// <summary>
/// Could not find any account info exception.
/// </summary>
public class AccountNotFoundException : Exception
{
    /// <summary>
    /// <see cref="AccountNotFoundException"/> constructor.
    /// </summary>
    public AccountNotFoundException()
    { }

    /// <summary>
    /// <see cref="AccountNotFoundException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public AccountNotFoundException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="AccountNotFoundException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public AccountNotFoundException(string message, Exception inner) : base(message, inner)
    { }

    /// <summary>
    /// Throw <see cref="AccountNotFoundException"/> if argument is null.
    /// </summary>
    /// <param name="argument">Any optional object.</param>
    /// <param name="message">Exception message.</param>
    /// <exception cref="AccountNotFoundException"></exception>
    public static void ThrowIfNull([NotNull] object? argument, string? message = null)
    {
        if (argument == null)
        {
            throw message == null ? new AccountNotFoundException() : new AccountNotFoundException(message);
        }
    }
}
