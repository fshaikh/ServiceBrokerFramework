using Microsoft.AspNetCore.Http;
using ServiceBroker.Shared;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ServiceBroker.Middleware.AuthProvider
{
    public class AuthenticationProviderMiddleware
    {
        #region Members
        /// <summary>
        /// Middleware delegate to call after this middleware finishes processing HTTP Request
        /// </summary>
        private readonly RequestDelegate _next;

        private readonly IAuthenticationProvider _authProvider;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Initializes anew instance of <see cref="AuthenticationProviderMiddleware"/>
        /// </summary>
        /// <param name="next">Object representing the next delegate in the HTTP request processing pipeline</param>
        public AuthenticationProviderMiddleware(RequestDelegate next,IAuthenticationProvider authProvider)
        {
            // If our middleware does not completely handle a request, the request’s context should be passed along to this next delegate.
            _next = next;

            _authProvider = authProvider;
        }
        #endregion Constructors

        #region Middleware Methods
        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Auth Middleware Invoked");
            AuthenticationResponse authResponse = this._authProvider.Validate(httpContext);
            if(!authResponse.IsSuccess || authResponse.ResponseCode == ResponseCode.UnAuthorized)
            {
                AuthenticationFailed(httpContext);
                return;
            }

            // Call the next middleware in the pipleine
            await _next.Invoke(httpContext);
        }
        #endregion Middleware Methods

        #region Helper Methods
        private async void AuthenticationFailed(HttpContext httpContext)
        {
            using (var writer = new StreamWriter(httpContext.Response.Body))
            {
                // return 401 Unauthorized
                httpContext.Response.StatusCode = 401;

                // Set the body response
                await writer.WriteAsync("Invalid Credentials");
            }
        }
        #endregion Helper Methods
    }
}
