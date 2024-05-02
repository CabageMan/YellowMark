namespace YellowMark.AppServices.UsersInfos.Exceptions;

/// <summary>
/// Could not find any user info exception.
/// </summary>
public class UserInfoNotFoundException : Exception
{
    /// <summary>
    /// <see cref="UserInfoNotFoundException"/> constructor.
    /// </summary>
    public UserInfoNotFoundException()
    { }

    /// <summary>
    /// <see cref="UserInfoNotFoundException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    public UserInfoNotFoundException(string message) : base(message)
    { }

    /// <summary>
    /// <see cref="UserInfoNotFoundException"/> constructor.
    /// </summary>
    /// <param name="message">Exception message.</param>
    /// <param name="inner">Inner <see cref="Exception"/></param>
    public UserInfoNotFoundException(string message, Exception inner) : base(message, inner)
    { }
}
