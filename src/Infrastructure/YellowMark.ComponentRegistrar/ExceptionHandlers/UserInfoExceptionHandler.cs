using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using YellowMark.AppServices.UsersInfos.Exceptions;

namespace YellowMark.ComponentRegistrar.ExceptionHandlers;

public class UserInfoExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is UserInfoNotFoundException userInfoNotFoundException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsJsonAsync(userInfoNotFoundException.Message, cancellationToken);
            return true;
        }

        return false;
    }
}
