using Microsoft.AspNetCore.Http;
using System.Net;

namespace MyNZWalks.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _requestDelegate;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger , RequestDelegate requestDelegate)
        {
            _logger = logger;
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext); 
            }
            catch (Exception e) 
            {
                var ErrorId = Guid.NewGuid();
                //Log this Exception
                _logger.LogError(e ,$"{ErrorId} : {e.Message}");
                //Return A Custom Error Reponse
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var error = new
                {
                    Id = ErrorId,
                    ErrorMessage = "Somthing went wrong! We are looking into resolving this.!"
                };
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
