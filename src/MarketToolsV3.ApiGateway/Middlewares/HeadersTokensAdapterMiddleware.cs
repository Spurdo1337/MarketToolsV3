﻿using MarketToolsV3.ApiGateway.Constant;
using MarketToolsV3.ApiGateway.Models;
using MarketToolsV3.ApiGateway.Services.Interfaces;
using Microsoft.Extensions.Options;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace MarketToolsV3.ApiGateway.Middlewares
{
    public class HeadersTokensAdapterMiddleware(RequestDelegate next)
    {
        public Task Invoke(HttpContext httpContext, 
            IAuthContext authContext)
        {
            if (authContext.State == AuthState.TokensRefreshed
                || authContext.State == AuthState.SessionActive)
            {
                httpContext.Request.Headers.Authorization = $"Bearer {authContext.AccessToken ?? string.Empty}";
            }

            return next(httpContext);
        }
    }
}
