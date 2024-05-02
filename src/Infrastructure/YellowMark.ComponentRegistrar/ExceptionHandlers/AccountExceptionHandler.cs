using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using YellowMark.AppServices.Accounts.Exceptions;

namespace YellowMark.ComponentRegistrar.ExceptionHandlers;

public class AccountExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is AccountNotFoundException accountNotFoundException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(accountNotFoundException.Message, cancellationToken);
            return true;
        }
        else if (exception is AccountBadRequestException accountBadRequestException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(accountBadRequestException.Message, cancellationToken);
            return true;
        }
        else if (exception is AccountOperationException accountOperationException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            await httpContext.Response.WriteAsJsonAsync(accountOperationException.Message, cancellationToken);
            return true;
        }

        return false;
    }
}
