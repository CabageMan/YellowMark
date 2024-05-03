using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using YellowMark.AppServices.Accounts.Exceptions;

namespace YellowMark.ComponentRegistrar.ExceptionHandlers;

/// <summary>
/// Account exceptions handler.
/// </summary>
public class AccountExceptionHandler : IExceptionHandler
{
    private readonly ILogger<AccountExceptionHandler> _logger;

    /// <summary>
    /// Constructor for <see cref="AccountExceptionHandler"/>
    /// </summary>
    /// <param name="logger"></param>
    public AccountExceptionHandler(ILogger<AccountExceptionHandler> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is AccountNotFoundException accountNotFoundException)
        {
            _logger.LogError("AccountNotFoundException: {Message}", accountNotFoundException.Message);

            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(accountNotFoundException.Message, cancellationToken);
            return true;
        }
        else if (exception is AccountBadRequestException accountBadRequestException)
        {
            _logger.LogError("AccountBadRequestException: {Message}", accountBadRequestException.Message);

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(accountBadRequestException.Message, cancellationToken);
            return true;
        }
        else if (exception is AccountOperationException accountOperationException)
        {
            _logger.LogError("AccountOperationException: {Message}", accountOperationException.Message);
            
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            await httpContext.Response.WriteAsJsonAsync(accountOperationException.Message, cancellationToken);
            return true;
        }

        return false;
    }
}
