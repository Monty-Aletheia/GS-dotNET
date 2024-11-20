using Application.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Middleware
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                BadRequestException => 400,
                UnauthorizedException => 401,
                NotFoundException => 404,
                _ => 500
            };

            var response = new ErrorResponse
            {
                Error = context.Exception.GetType().Name,
                Message = context.Exception.Message,
                Details = statusCode == 500 ? "An unexpected error occurred." : null
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = statusCode
            };

            Console.WriteLine($"Exception: {context.Exception}");
        }

        public class ErrorResponse
        {
            public string Error { get; set; }
            public string Message { get; set; }
            public string Details { get; set; }
        }
    }
}
