using Newtonsoft.Json;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Success Response object for Service Provision request
    /// </summary>
    public class ServiceProvisionResponse : ResponseBase
    {
        #region Public Properties
        /// <summary>
        /// Gets/Sets success response.
        /// For asynchronous responses, service brokers MAY return an identifier representing the operation.
        /// The value of this field MUST be provided by the platform with requests to the Last Operation
        /// endpoint in a URL encoded query parameter. If present, MUST be a non-empty string.
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        /// The URL of a web-based management user interface for the service instance;
        /// we refer to this as a service dashboard. The URL MUST contain enough information for the
        /// dashboard to identify the resource being accessed (9189kdfsk0vfnku in the example below).
        /// Note: a broker that wishes to return dashboard_url for a service instance MUST return it
        /// with the initial response to the provision request, even if the service is provisioned
        /// asynchronously. If present, MUST be a non-empty string.
        /// </summary>
        [JsonProperty("dashboard_url")]
        public string DashboardUrl { get; set; }

        
        #endregion Public Properties
    }
}
