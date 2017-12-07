namespace ServiceBroker.Shared
{
    /// <summary>
    /// Represents a service catalog response sent by a provider
    /// </summary>
    public class ServiceCatalogResponse : ResponseBase
    {
        #region Public Properties
        /// <summary>
        /// Gets/Sets the catalog data
        /// </summary>
        public object CatalogData { get; set; }
        #endregion Public Properties
    }
}
