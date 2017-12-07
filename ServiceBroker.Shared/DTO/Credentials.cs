using Newtonsoft.Json;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// This class represents the Credentials object to be used when sending a response to a binding request
    /// </summary>
    public class Credentials
    {
        #region Public Properties
        /// <summary>
        /// Gets/Sets the Uri of the resource
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Gets/Sets the user name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets/Sets the password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets/Sets the host
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Gets/Sets the port
        /// 
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets/Sets the backing store name. Could be a database name, queue name, etc
        /// </summary>
        [JsonProperty("database")]
        public string BackingStoreName { get; set; }

        #endregion Public Properties
    }
}
