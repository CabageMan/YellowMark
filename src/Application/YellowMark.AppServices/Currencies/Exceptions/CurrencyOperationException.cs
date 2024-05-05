namespace YellowMark.AppServices.Currencies.Exceptions;

/// <summary>
/// Currency operation exception.
/// </summary>
public class CurrencyOperationException : Exception
{
    /// <summary>
    /// <see cref="CurrencyOperationException"/> constructor.
    /// </summary>
    public CurrencyOperationException()
    { }

    /// <summary>
    /// <see cref="CurrencyOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public CurrencyOperationException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="CurrencyOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public CurrencyOperationException(string message, Exception inner) : base(message, inner)
    { }
}
