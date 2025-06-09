using myCleanArchitecture.Shared.Results;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace myCleanArchitecture.UI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(IConfiguration configuration, RequestDelegate requestDelegate)
        {
            _configuration = configuration;
            _requestDelegate = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpResponse httpResponse = httpContext.Response;
            httpResponse.ContentType = "application/json";
            Result customResult;
            switch (ex)
            {
                case UnauthorizedAccessException:
                    customResult = new Result(Meta.Unauthorized(ex.Message));
                    httpResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
                    string loginUrl = _configuration["LogoutURL"] ?? "/Auth/Logout";
                    httpContext.Response.Redirect(loginUrl);
                    return;
                case ValidationException:
                    customResult = new Result(Meta.Unauthorized(ex.Message));
                    httpResponse.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case KeyNotFoundException:
                    customResult = new Result(Meta.NotFound(ex.Message));
                    httpResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case BadHttpRequestException:
                    string serverErrorUrl;
                    HandleBadRequest(httpContext, ex, httpResponse, out customResult, out serverErrorUrl, ex.Message);
                    break;
                default:
                    string message = ex.Message;
                    message += ex.InnerException is null ? "" : "\n" + ex.InnerException.Message;
                    HandleBadRequest(httpContext, ex, httpResponse, out customResult, out serverErrorUrl, message);
                    break;
            }
            var res = JsonConvert.SerializeObject(customResult);
            await httpResponse.WriteAsync(res);
        }

        private void HandleBadRequest(HttpContext httpContext, Exception ex, HttpResponse httpResponse, out Result customResult, out string serverErrorUrl, string message)
        {
            customResult = new Result(Meta.BadRequest(ex.Message));
            httpResponse.StatusCode = (int)HttpStatusCode.BadRequest;
            serverErrorUrl = _configuration["ServerErrorUrl"] += "?errorMessage=" + message;
            httpContext.Response.Redirect(serverErrorUrl);
        }
    }
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
