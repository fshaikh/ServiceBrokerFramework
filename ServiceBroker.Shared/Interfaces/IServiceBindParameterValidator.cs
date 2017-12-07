namespace ServiceBroker.Shared
{
    /// <summary>
    /// Validator for Service Bind Parameters.
    /// Brokers SHOULD ensure that the client has provided valid configuration parameters and values
    /// for the operation.
    /// </summary>
    public interface IServiceBindParameterValidator
    {
        /// <summary>
        /// Validates the supplied parameters
        /// </summary>
        /// <param name="request">Request object</param>
        /// <returns>True if valid, else false</returns>
        bool IsValid(ServiceBindRequest request);
    }
}
