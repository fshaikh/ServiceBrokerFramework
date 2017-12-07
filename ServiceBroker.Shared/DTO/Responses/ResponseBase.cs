using Newtonsoft.Json;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Represents an base response object
    /// </summary>
    public class ResponseBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="ResponseBase"/>
        /// </summary>
        public ResponseBase()
        {
            IsSuccess = true;
        }
        #endregion Constructors
        #region Public Properties
        /// <summary>
        /// Gets/Sets a meaningful error message explaining why the request failed.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets/Sets whether the response is error or success
        /// </summary>
        [JsonIgnore()]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets/Sets the error code/short text
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets/Sets the response code.
        /// </summary>
        [JsonIgnore()]
        public ResponseCode ResponseCode { get; set; }
        #endregion Public Properties
    }
}
