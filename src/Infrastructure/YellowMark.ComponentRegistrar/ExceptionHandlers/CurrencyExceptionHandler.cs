using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using YellowMark.AppServices.Currencies.Exceptions;

namespace YellowMark.ComponentRegistrar.ExceptionHandlers;

/// <summary>
/// Currency exceptions handler.
/// </summary>
public class CurrencyExceptionHandler : IExceptionHandler
{
    private readonly ILogger<CurrencyExceptionHandler> _logger;

    /// <summary>
    /// Constructor for <see cref="CurrencyExceptionHandler"/>
    /// </summary>
    /// <param name="logger">Logger.</param>
    public CurrencyExceptionHandler(ILogger<CurrencyExceptionHandler> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is CurrencyOperationException currencyOperationException)
        {
            _logger.LogError("CurrencyOperationException: {Message}", currencyOperationException.Message);

            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            await httpContext.Response.WriteAsJsonAsync(currencyOperationException.Message, cancellationToken);
            return true;
        }

        return false;
    }
}
