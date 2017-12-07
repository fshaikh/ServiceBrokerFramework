namespace ServiceBroker.Shared
{
    /// <summary>
    /// Represents response object for credentials type binding
    /// </summary>
    public class CredentialsBindingResponse:ResponseBase
    {
        #region Public Properties
        /// <summary>
        /// Gets/Sets the credentials to connect to the resource
        /// </summary>
        public Credentials Credentials { get; set; }
        #endregion Public Properties
    }
}
