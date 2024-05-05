using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using YellowMark.AppServices.Ads.Exceptions;

namespace YellowMark.ComponentRegistrar.ExceptionHandlers;

/// <summary>
/// Ad exceptions handler.
/// </summary>
public class AdExceptionHandler : IExceptionHandler
{
    private readonly ILogger<AdExceptionHandler> _logger;

    /// <summary>
    /// Constructor for <see cref="AdExceptionHandler"/>
    /// </summary>
    /// <param name="logger"></param>
    public AdExceptionHandler(ILogger<AdExceptionHandler> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is AdNotFoundException adNotFoundException)
        {
            _logger.LogError("AdNotFoundException: {Message}", adNotFoundException.Message);

            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(adNotFoundException.Message, cancellationToken);
            return true;
        }
        else if (exception is AdOperationException adOperationException)
        {
            _logger.LogError("AdOperationException: {Message}", adOperationException.Message);
            
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            await httpContext.Response.WriteAsJsonAsync(adOperationException.Message, cancellationToken);
            return true;
        }
        else if (exception is AdPermissionsDeniedException adPermissionsDeniedException)
        {
            _logger.LogError("AdPermissionsDeniedException: {Message}", adPermissionsDeniedException.Message);
            
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            await httpContext.Response.WriteAsJsonAsync(adPermissionsDeniedException.Message, cancellationToken);
            return true;
        }

        return false;
    }
}