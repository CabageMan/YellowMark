using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using YellowMark.AppServices.Files.Exceptions;

namespace YellowMark.ComponentRegistrar.ExceptionHandlers;

/// <summary>
/// File exceptions handler.
/// </summary>
public class FileExceptionHandler : IExceptionHandler
{
    private readonly ILogger<FileExceptionHandler> _logger;

    /// <summary>
    /// Constructor for <see cref="FileExceptionHandler"/>
    /// </summary>
    /// <param name="logger">Logger.</param>
    public FileExceptionHandler(ILogger<FileExceptionHandler> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is FileOperationException fileOperationException)
        {
            _logger.LogError("FileOperationException: {Message}", fileOperationException.Message);

            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            await httpContext.Response.WriteAsJsonAsync(fileOperationException.Message, cancellationToken);
            return true;
        }

        return false;
    }
}
