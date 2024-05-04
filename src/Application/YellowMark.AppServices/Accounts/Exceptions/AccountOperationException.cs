using System.Diagnostics.CodeAnalysis;

namespace YellowMark.AppServices.Accounts.Exceptions;

/// <summary>
/// Account operation exception.
/// </summary>
public class AccountOperationException : Exception {
    /// <summary>
    /// <see cref="AccountOperationException"/> constructor.
    /// </summary>
    public AccountOperationException()
    { }

    /// <summary>
    /// <see cref="AccountOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public AccountOperationException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="AccountOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public AccountOperationException(string message, Exception inner) : base(message, inner)
    { }

    /// <summary>
    /// Throw <see cref="AccountOperationException"/> if argument is null.
    /// </summary>
    /// <param name="argument">Any optional object.</param>
    /// <param name="message">Exception message.</param>
    /// <exception cref="AccountOperationException"></exception>
    public static void ThrowIfNull([NotNull] object? argument, string? message = null)
    {
        if (argument == null)
        {
            throw message == null ? new AccountOperationException() : new AccountOperationException(message);
        }
    }
}
