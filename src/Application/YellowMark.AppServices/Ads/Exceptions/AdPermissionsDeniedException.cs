namespace YellowMark.AppServices.Ads.Exceptions;

/// <summary>
/// Ad permissions denied exception.
/// </summary>
public class AdPermissionsDeniedException : Exception {
    /// <summary>
    /// <see cref="AdPermissionDeniedException"/> constructor.
    /// </summary>
    public AdPermissionsDeniedException()
    { }

    /// <summary>
    /// <see cref="AdPermissionDeniedException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public AdPermissionsDeniedException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="AdPermissionDeniedException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public AdPermissionsDeniedException(string message, Exception inner) : base(message, inner)
    { }
}

