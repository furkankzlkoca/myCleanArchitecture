using myCleanArchitecture.Shared.Results;
using System.Net;

namespace myCleanArchitecture.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {            
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            Result customResult;
            switch (exception)
            {
                case UnauthorizedAccessException:
                    customResult = new Result(Meta.Unauthorized(exception.Message));
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case System.ComponentModel.DataAnnotations.ValidationException:
                case FluentValidation.ValidationException:
                    customResult = new Result(Meta.ValidationError(exception.Message));
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case KeyNotFoundException:
                    customResult = new Result(Meta.NotFound(exception.Message));
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    string message = exception.Message ?? "An unexpected error occurred.";
                    message += exception.InnerException != null ? $" Inner Exception: {exception.InnerException.Message}" : string.Empty;
                    customResult = new Result(Meta.ServerError(message));
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            await response.WriteAsJsonAsync(customResult);
        }
    }
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
