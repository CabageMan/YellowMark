namespace YellowMark.AppServices.Files.Exceptions;

/// <summary>
/// File operation exception.
/// </summary>
public class FileOperationException : Exception {
    /// <summary>
    /// <see cref="FileOperationException"/> constructor.
    /// </summary>
    public FileOperationException()
    { }

    /// <summary>
    /// <see cref="FileOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public FileOperationException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="FileOperationException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public FileOperationException(string message, Exception inner) : base(message, inner)
    { }

    /// <summary>
    /// Throw <see cref="FileOperationException"/> if argument is FALSE.
    /// </summary>
    /// <param name="argument">Bool value.</param>
    /// <param name="message">Exception message.</param>
    /// <exception cref="FileOperationException"></exception>
    public static void ThrowIfFalse(bool argument, string? message = null)
    {
        if (!argument)
        {
            throw message == null ? new FileOperationException() : new FileOperationException(message);
        }
    }
}