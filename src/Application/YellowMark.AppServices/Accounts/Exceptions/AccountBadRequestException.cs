﻿namespace YellowMark.AppServices.Accounts.Exceptions;

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
}
