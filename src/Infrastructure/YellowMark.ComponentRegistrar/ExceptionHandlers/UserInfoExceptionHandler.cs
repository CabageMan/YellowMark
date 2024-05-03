using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using YellowMark.AppServices.UsersInfos.Exceptions;

namespace YellowMark.ComponentRegistrar.ExceptionHandlers;

/// <summary>
/// User info exceptions handler.
/// </summary>
public class UserInfoExceptionHandler : IExceptionHandler
{
    private readonly ILogger<UserInfoExceptionHandler> _logger;

    /// <summary>
    /// Constructor for <see cref="UserInfoExceptionHandler"/>
    /// </summary>
    /// <param name="logger">Logger.</param>
    public UserInfoExceptionHandler(ILogger<UserInfoExceptionHandler> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is UserInfoNotFoundException userInfoNotFoundException)
        {
            _logger.LogError("UserInfoNotFoundException: {Message}", userInfoNotFoundException.Message);

            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(userInfoNotFoundException.Message, cancellationToken);
            return true;
        }

        return false;
    }
}
