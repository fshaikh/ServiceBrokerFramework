namespace ServiceBroker.Shared
{
    public enum LastOperationResponseCode
    {
        /// <summary>
        /// MUST be returned upon successful processing of this request.
        /// </summary>
        Success = 200,

        /// <summary>
        /// Appropriate only for asynchronous delete operations. The platform MUST consider this
        /// response a success and remove the resource from its database.
        /// </summary>
        Gone = 410
    }
}
