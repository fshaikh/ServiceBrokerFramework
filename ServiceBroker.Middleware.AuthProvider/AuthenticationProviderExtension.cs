using Microsoft.AspNetCore.Builder;

namespace ServiceBroker.Middleware.AuthProvider
{
    public static class AuthenticationProviderExtension
    {
        /// <summary>
        /// Adds an extension method for our custom middleware.This makes adding them easy while setting up middlewares
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseServiceBrokerAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationProviderMiddleware>();
        }
    }
}
