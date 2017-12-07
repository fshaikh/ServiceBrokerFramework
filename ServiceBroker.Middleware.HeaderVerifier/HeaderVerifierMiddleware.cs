using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBroker.Middleware.HeaderVerifier
{
    /// <summary>
    /// Middleware for verifying Cloud Foundry Service Broker API Version Header.
    /// Requests from the platform to the service broker MUST contain a header that declares the version number of the Service Broker API that the marketplace will use:
    /// X-Broker-Api-Version: 2.12 
    /// The version numbers are in the format MAJOR.MINOR using semantic versioning.This header allows brokers to reject requests from marketplaces
    /// for versions they do not support.While minor API revisions will always be additive, it is possible that brokers depend on a feature from
    /// a newer version of the API that is supported by the platform.
    /// In this scenario the broker MAY reject the request with 412 Precondition Failed and provide a message that informs the operator of the
    /// API version that is to be used instead.
    /// </summary>
    public class HeaderVerifierMiddleware
    {
        #region Constants
        /// <summary>
        /// API Version Header Key
        /// </summary>
        private const string APIVERSION_HEADER = "X-Broker-Api-Version";
        /// <summary>
        /// Supported Service Broker API Version
        /// </summary>
        private const string SUPPORTED_APIVERSION = "2.13";  // TODO: Read from envrionment variable

        /// <summary>
        /// Message that informs the operator of the API version that is to be used instead.
        /// </summary>
        private const string HEADER_APIVERSION_MISSING_MESSAGE = "API Version Header Missing";

        /// <summary>
        /// Message that informs the operator of the API version that is to be used instead.
        /// </summary>
        private const string UNSUPPORTED_APIVERSION_MESSAGE = "Unsupported API Version. Use {0} version";
        #endregion Constants

        #region Members
        /// <summary>
        /// Middleware delegate to call after this middleware finishes processing HTTP Request
        /// </summary>
        private readonly RequestDelegate _next;
        #endregion Members


        #region Constructors
        /// <summary>
        /// Initializes anew instance of <see cref="HeaderVerifierMiddleware"/>
        /// </summary>
        /// <param name="next">Object representing the next delegate in the HTTP request processing pipeline</param>
        public HeaderVerifierMiddleware(RequestDelegate next)
        {
            // If our middleware does not completely handle a request, the request’s context should be passed along to this next delegate.
            _next = next;
        }
        #endregion Constructors

        #region Middleware Methods
        /// <summary>
        /// All middlewares are expected to have Invoke method to handle incoming HTTP request. Will be invoked by ASPNET Core
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine($"Request for {httpContext.Request.Path}");

            // 1. Check if the API Version Header is present. If not, return error
            if (!httpContext.Request.Headers.ContainsKey(APIVERSION_HEADER))
            {
                Console.WriteLine(string.Format("Header {0} missing in Request", APIVERSION_HEADER));
                PreconditionFailed(httpContext,HEADER_APIVERSION_MISSING_MESSAGE);
                return;
            }

            // 2. If the API version is not supported, return error
            if(!IsSupportedVersion(httpContext))
            {
                Console.WriteLine(string.Format("Header {0} version in Request", APIVERSION_HEADER));
                PreconditionFailed(httpContext, string.Format(UNSUPPORTED_APIVERSION_MESSAGE, SUPPORTED_APIVERSION));
                return;
            }

            // Call the next middleware in the pipeline
            await _next.Invoke(httpContext);
        }


        #endregion Middleware Methods

        #region Helper Methods
        /// <summary>
        /// Verifies if we are supporting the Service Broker API version
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns>True/False </returns>
        private bool IsSupportedVersion(HttpContext httpContext)
        {
            var value = httpContext.Request.Headers[APIVERSION_HEADER];
            Console.WriteLine("API Version: " + value);
            return value == SUPPORTED_APIVERSION ? true : false;
        }

        private async void PreconditionFailed(HttpContext httpContext,string message)
        {
            using (var writer = new StreamWriter(httpContext.Response.Body))
            {
                // return 412 Precondition Failed
                httpContext.Response.StatusCode = 412;

                // Set the body response
                await writer.WriteAsync(message);
            }
        }
        #endregion Helper Methods
    }
}
