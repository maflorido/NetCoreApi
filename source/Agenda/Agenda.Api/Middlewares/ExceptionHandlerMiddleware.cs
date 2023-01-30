using Agenda.Domain.Exceptions;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net;
using System.Text;

namespace Agenda.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;
            _logger = logFactory.CreateLogger("MyMiddleware");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            httpContext.Response.ContentType = "application/json";
            dynamic patternResponse = new ExpandoObject();
            var originalResponse = httpContext.Response.Body;
            var newResponse = new MemoryStream();
            httpContext.Response.Body = newResponse;

            try
            {
                await _next(httpContext);

                patternResponse.StatusCode = httpContext.Response.StatusCode;
                patternResponse.Success = true;

                httpContext.Response.Body.Position = 0;
                string responseBody = new StreamReader(httpContext.Response.Body, Encoding.UTF8).ReadToEnd();
                patternResponse.Object = JsonConvert.DeserializeObject(responseBody);
            }
            catch (NotfoundException e)
            {
                patternResponse.StatusCode = (int)HttpStatusCode.NotFound;
                patternResponse.Success = false;
                patternResponse.ErrorMessage = e.Message;
            }
            catch (ValidationException e)
            {
                patternResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                patternResponse.Success = false;
                patternResponse.ErrorMessage = e.Message;

            }
            catch (Exception e)
            {
                patternResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                patternResponse.Success = false;
                patternResponse.ErrorMessage = e.Message;

            }
            
            httpContext.Response.Body = originalResponse;
            newResponse.Position = 0;

            string result = JsonConvert.SerializeObject(patternResponse);
            await httpContext.Response.WriteAsync(result);
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }

    public class ResultPatternResponse
    {
        public ResultPatternResponse(bool success, int statusCode, string errorMessage, dynamic @object)
        {
            Success = success;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
            Object = @object;
        }

        public ResultPatternResponse(bool success, int statusCode, string errorMessage)
        {
            Success = success;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public dynamic Object { get; set; }
    }
}
