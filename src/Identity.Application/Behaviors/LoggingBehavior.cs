﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Identity.Application.Behaviors
{
    internal class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling {request}", typeof(TRequest).Name);
            logger.LogDebug("Request body {@body}", request);
            
            var response = await next(cancellationToken);

            logger.LogDebug("Response body {@body}", response);
            logger.LogInformation("Handled {request}", typeof(TResponse).Name);

            return response;
        }
    }
}
