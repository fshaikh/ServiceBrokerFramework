namespace ServiceBroker.Shared
{
    /// <summary>
    /// Represents Metadata information for a service.
    /// </summary>
    public class ServiceMetadata
    {
        #region Public Properties
        /// <summary>
        /// Gets/Sets name of the service to be displayed in graphical clients.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets/Sets URL to an image
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets/Sets Long description
        /// </summary>
        public string LongDescription { get; set; }

        /// <summary>
        /// Gets/Sets name of the upstream entity providing the actual service.
        /// </summary>
        public string ProviderDisplayName { get; set; }

        /// <summary>
        /// Gets/Sets Link to documentation page for the service
        /// </summary>
        public string DocumentationUrl { get; set; }

        /// <summary>
        /// Gets/Sets Link to support page for the service
        /// </summary>
        public string SupportUrl { get; set; }
        #endregion Public Properties
    }
}
