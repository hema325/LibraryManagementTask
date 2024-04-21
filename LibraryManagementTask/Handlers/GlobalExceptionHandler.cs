using LibraryManagementTask.Errors;
using Microsoft.AspNetCore.Diagnostics;

namespace LibraryManagementTask.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var result = new ExceptionResponse(exception?.InnerException?.Message ?? exception.Message,
                exception?.StackTrace?.ToString());

            httpContext.Response.StatusCode = result.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(result);

            return true;
        }
    }
}
