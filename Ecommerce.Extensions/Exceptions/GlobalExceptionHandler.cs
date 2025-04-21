using Ecommerce.Extensions.Requests;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Extensions.Exceptions;

public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger
) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError("Exception: {error}", exception.Message);

        httpContext.Response.ContentType = "application/json";

        if (exception is ResponseException resultException)
        {
            httpContext.Response.StatusCode = exception switch
            {
                ResponseValidationException => StatusCodes.Status400BadRequest,
                UnauthorizedException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status422UnprocessableEntity
            };

            var errorResponse = new EndpointResult<object>
            {
                IsSuccess = false,
                ErrorCode = resultException.Code,
                ErrorMessage = resultException.Message,
                Data = resultException.AdditionalData
            };

            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
        }
        else
        {
            httpContext.Response.StatusCode = exception switch
            {
                UnauthorizedAccessException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            ProblemDetails problemDetails = new()
            {
                Status = httpContext.Response.StatusCode,
                Title = "Internal server error",
                Detail = exception.Message
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        }

        return true;
    }
}