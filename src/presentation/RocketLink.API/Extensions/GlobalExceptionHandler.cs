using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RocketLink.Domain.Common;

namespace RocketLink.API.Extensions;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

        //var problemDetails = new ProblemDetails
        //{
        //    Status = StatusCodes.Status500InternalServerError,
        //    Title = "Server error"
        //};

        var error = new Error("Error", "Internal Server Error");

        var result = Result.Failure(error);

        httpContext.Response.StatusCode = 500;

        await httpContext.Response
            .WriteAsJsonAsync(result, cancellationToken);

        return true;
    }
}