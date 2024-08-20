using MediatR;
using Microsoft.Extensions.Logging;
using RocketLink.Domain.Common;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLink.Application.Behaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        logger.LogInformation(
            "Processing request {RequestName}",
            requestName);

        TResponse result = await next();

        if (result.IsSuccess)
        {
            logger.LogInformation(
                "Completed request {RequestName}",
                requestName);
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                logger.LogError(
                    "Completed request {RequestName} with error",
                    requestName);
            }
        }

        return result;
    }
}