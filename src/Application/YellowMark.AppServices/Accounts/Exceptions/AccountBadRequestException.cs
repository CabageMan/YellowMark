using System.Diagnostics.CodeAnalysis;

namespace YellowMark.AppServices.Accounts.Exceptions;

/// <summary>
/// Account bad requet exception.
/// </summary>
public class AccountBadRequestException : Exception
{
    /// <summary>
    /// <see cref="AccountBadRequestException"/> constructor.
    /// </summary>
    public AccountBadRequestException()
    { }

    /// <summary>
    /// <see cref="AccountBadRequestException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public AccountBadRequestException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="AccountBadRequestException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public AccountBadRequestException(string message, Exception inner) : base(message, inner)
    { }

    /// <summary>
    /// Throw <see cref="AccountBadRequestException"/> if argument is false.
    /// </summary>
    /// <param name="argument">Bool value</param>
    /// <param name="message">Exception message.</param>
    /// <exception cref="AccountBadRequestException"></exception>
    public static void ThrowIfFalse(bool argument, string? message = null)
    {
        if (!argument)
        {
            throw message == null ? new AccountBadRequestException() : new AccountBadRequestException(message);
        }
    }
}
