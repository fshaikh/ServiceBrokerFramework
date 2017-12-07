namespace ServiceBroker.Shared
{
    /// <summary>
    /// This represents enumeration for Service Provision response code
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// MUST be returned if the service instance already exists, is fully provisioned, and the
        /// requested parameters are identical to the existing service instance
        /// </summary>
        AlreadyExists = 200,
        SuccessfulDelete = 200,
        ValidCredentials = 200,

        /// <summary>
        /// MUST be returned if the service instance was provisioned as a result of provision request.
        /// </summary>
        SuccessfulProvision = 201,

        /// <summary>
        /// MUST be returned if the service instance provisioning is in progress. This triggers
        /// the platform marketplace to poll the Service Instance Last Operation Endpoint for
        /// operation status.
        /// </summary>
        ProvisioningInProgress = 202,

        /// <summary>
        /// MUST be returned if a service instance with the same id already exists but with
        /// different attributes
        /// </summary>
        Conflict = 409,

        /// <summary>
        /// 
        /// </summary>

        UnprocessableEntity = 422,

        /// <summary>
        /// Indicates that the authentication for the API request has failed
        /// </summary>
        UnAuthorized = 401


    }
}
