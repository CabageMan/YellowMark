
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using YellowMark.AppServices.Comments.Exceptions;

namespace YellowMark.ComponentRegistrar.ExceptionHandlers;

/// <summary>
/// Comment exceptions handler.
/// </summary>
public class CommentExceptionHandler : IExceptionHandler
{
    private readonly ILogger<CommentExceptionHandler> _logger;

    /// <summary>
    /// Constructor for <see cref="CommentExceptionHandler"/>
    /// </summary>
    /// <param name="logger"></param>
    public CommentExceptionHandler(ILogger<CommentExceptionHandler> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is CommentNotFoundException commentNotFoundException)
        {
            _logger.LogError("CommentNotFoundException: {Message}", commentNotFoundException.Message);

            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(commentNotFoundException.Message, cancellationToken);
            return true;
        }
        else if (exception is CommentOperationException commentOperationException)
        {
            _logger.LogError("CommentOperationException: {Message}", commentOperationException.Message);
            
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            await httpContext.Response.WriteAsJsonAsync(commentOperationException.Message, cancellationToken);
            return true;
        }
        else if (exception is CommentPermissionsDeniedException commentPermissionsDeniedException)
        {
            _logger.LogError("CommentPermissionsDeniedException: {Message}", commentPermissionsDeniedException.Message);
            
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            await httpContext.Response.WriteAsJsonAsync(commentPermissionsDeniedException.Message, cancellationToken);
            return true;
        }

        return false;
    }
}