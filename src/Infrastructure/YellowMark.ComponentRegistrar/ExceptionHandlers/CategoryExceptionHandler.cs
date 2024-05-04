using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using YellowMark.AppServices.Categories.Exceptions;

namespace YellowMark.ComponentRegistrar.ExceptionHandlers;

/// <summary>
/// Category exceptions handler.
/// </summary>
public class CategoryExceptionHandler : IExceptionHandler
{
    private readonly ILogger<CategoryExceptionHandler> _logger;

    /// <summary>
    /// Constructor for <see cref="CategoryExceptionHandler"/>
    /// </summary>
    /// <param name="logger">Logger.</param>
    public CategoryExceptionHandler(ILogger<CategoryExceptionHandler> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is CategoryOperationException categoryOperationException)
        {
            _logger.LogError("CategoryOperationException: {Message}", categoryOperationException.Message);

            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            await httpContext.Response.WriteAsJsonAsync(categoryOperationException.Message, cancellationToken);
            return true;
        } if (exception is CategoryNotFoundException categoryNotFoundException)
        {
            _logger.LogError("CategoryNotFoundException: {Message}", categoryNotFoundException.Message);

            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(categoryNotFoundException.Message, cancellationToken);
            return true;
        }

        return false;
    }
}
