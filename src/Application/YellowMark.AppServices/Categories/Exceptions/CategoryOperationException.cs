namespace YellowMark.AppServices.Categories.Exceptions;

/// <summary>
/// Category operation exception.
/// </summary>
public class CategoryOperationException : Exception
{
    /// <summary>
    /// <see cref="CategoryOperationException"/> constructor.
    /// </summary>
    public CategoryOperationException()
    { }

    /// <summary>
    /// <see cref="CategoryOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public CategoryOperationException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="CategoryOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public CategoryOperationException(string message, Exception inner) : base(message, inner)
    { }
}
