using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBroker.Middleware.HeaderVerifier
{
    /// <summary>
    /// Defines extension methods
    /// </summary>
    public static class HeaderVerifierExtension
    {
        /// <summary>
        /// Adds an extension method for our custom middleware.This makes adding them easy while setting up middlewares
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHeaderVerifier(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HeaderVerifierMiddleware>();
        }
    }
}
