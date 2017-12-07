using System.Collections.Generic;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Validator for Service Provision Parameters.
    /// Brokers SHOULD ensure that the client has provided valid configuration parameters and values
    /// for the operation.
    /// </summary>
    public interface IServiceProvisionParameterValidator
    {
        /// <summary>
        /// Validates the supplied parameters
        /// </summary>
        /// <param name="request">Request object</param>
        /// <returns>True if valid, else false</returns>
        List<ResponseBase> IsValid(ServiceProvisionRequest request);
    }
}
