using DevPartners.Sorted.Application.Exceptions;
using DevPartners.Sorted.Application.Models;
using DevPartners.Sorted.Application.Models.Errors;
using Microsoft.AspNetCore.Diagnostics;

namespace DevPartners.Sorted.Api.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync
    (
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken
    )
    {
        _logger.LogError("{message}", exception.Message);

        await ExceptionHandler(httpContext, exception, cancellationToken);

        return true;
    }

    private static Task ExceptionHandler(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        var status = context.Response.StatusCode;
        var exceptionType = exception.GetType();
        var errorObject = new ErrorResponse
        {
            Message = exception.Message,
            Detail = new List<ErrorDetail>()
        };

        if (exceptionType == typeof(RainfallService404Exception))
        {
            var error = exception.Message.Split('|');

            errorObject = new ErrorResponse
            {
                Message = error[1],
                Detail = new List<ErrorDetail> 
                { 
                    new() 
                    { 
                        PropertyName = "stationId", 
                        Message = $"Station Id {error[2]} not found" 
                    } 
                }
            };

            status = int.Parse(error[0]);
        }

        if (exceptionType == typeof(BadHttpRequestException))
        {
            var countParam = context.Request.Query["count"];
            errorObject = new ErrorResponse
            {
                Message = "One or more validation errors occurred.",
                Detail = new List<ErrorDetail>
                {
                    new()
                    {
                        PropertyName = "count",
                        Message = $"?count={countParam} should be an integer."
                    }
                }
            };
        }

        context.Response.StatusCode = status;
        return context.Response.WriteAsJsonAsync(errorObject, cancellationToken);
    }
}
