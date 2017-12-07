namespace ServiceBroker.Shared
{
    /// <summary>
    /// Interface which defines methods for provisioning/deprovisioning service broker request
    /// </summary>
    public interface IServiceProvisionProvider
    {
        /// <summary>
        /// Gets/Sets whether the service supports incomplete request
        /// From the SPEC:
        /// Platforms expect prompt responses to all API requests in order to provide users with fast feedback. Service broker authors
        /// SHOULD implement their brokers to respond promptly to all requests but will need to decide whether to implement
        /// synchronous or asynchronous responses. Brokers that can guarantee completion of the requested operation with the
        /// response SHOULD return the synchronous response. Brokers that cannot guarantee completion of the operation with
        /// the response SHOULD implement the asynchronous response.
        ///    Providing a synchronous response for a provision, update, or bind operation before actual completion causes confusion
        /// for users as their service might not be usable and they have no way to find out when it will be. Asynchronous responses 
        /// set expectations for users that an operation is in progress and can also provide updates on the status of the operation.
        ///    Support for synchronous or asynchronous responses MAY vary by service offering, even by service plan.
        /// </summary>
        bool SupportAsynchronousRequest(ServiceProvisionRequest request);

        /// <summary>
        /// Handles service provisioning request
        /// </summary>
        /// <param name="request">Request object</param>
        /// <returns>Response object</returns>
        ServiceProvisionResponse ProvisionService(ServiceProvisionRequest request);

        /// <summary>
        /// Handles service Deprovisioning request
        /// </summary>
        /// <param name="request">Request object</param>
        /// <returns>Response object</returns>
        ServiceProvisionResponse DeprovisionService(ServiceProvisionRequest request);

        /// <summary>
        /// Derived classes must implement this when service supports asynchronous operation for service provision/deprovision
        /// Return the status of the pending operation
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        LastOperationProvisionResponse GetLastOperationResponse(ServiceProvisionRequest request);
    }
}
