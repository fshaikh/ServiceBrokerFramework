using Newtonsoft.Json;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Represents object contains platform specific information related to the context in which the
    /// service will be used. In some cases the platform might not be able to provide this information
    /// at the time of the binding request, therefore the bind_resource and its fields are OPTIONAL.
    /// Below are some common fields that MAY be used.Platforms MAY choose to add additional ones as needed.
    /// </summary>
    public class BindResource
    {
        #region Public Properties
        /// <summary>
        /// Gets/Sets GUID of an application associated with the binding. For credentials bindings.
        /// </summary>
        [JsonProperty("app_guid")]
        public string AppGuid { get; set; }

        /// <summary>
        /// Gets/Sets URL of the application to be intermediated. For route services bindings.
        /// </summary>

        public string Route { get; set; }
        #endregion Public Properties
    }
}
