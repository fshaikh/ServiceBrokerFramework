using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Response object for Last Operation Service Provision request
    /// </summary>
    public class LastOperationProvisionResponse : ResponseBase
    {
        #region Public Properties
        /// <summary>
        /// Gets/Sets the status code for last operation request from platform
        /// </summary>
        [JsonConverter(typeof(LastOperationStateConverter))]
        public LastOperationStateCode State { get; set; }

        /// <summary>
        /// Gets/Sets the service provision response code. Return one of the 2 values : 
        /// </summary>
        [JsonIgnore()]
        public LastOperationResponseCode ResponseCode { get; set; }
        #endregion Public Properties
    }
}
