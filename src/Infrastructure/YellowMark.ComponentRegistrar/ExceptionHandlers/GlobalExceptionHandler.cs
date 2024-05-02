using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace YellowMark.ComponentRegistrar.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
