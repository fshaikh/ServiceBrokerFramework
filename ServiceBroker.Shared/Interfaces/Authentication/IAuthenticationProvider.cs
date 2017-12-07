using Microsoft.AspNetCore.Http;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Implementers must realize the interface to provide authentication functionality for Service Broker
    /// </summary>
    public interface IAuthenticationProvider
    {
        /// <summary>
        /// Validates the Credentials
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        AuthenticationResponse Validate(HttpContext httpContext);
    }
}
