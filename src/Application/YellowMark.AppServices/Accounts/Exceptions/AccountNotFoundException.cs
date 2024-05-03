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
}
