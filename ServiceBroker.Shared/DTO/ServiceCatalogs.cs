using System.Collections.Generic;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Represents a collection of service catalogs
    /// </summary>
    public class ServiceCatalogs
    {
        #region Members
        private List<Catalog> _services = new List<Catalog>();
        #endregion Members

        #region Public Properties
        /// <summary>
        /// Gets/Sets services
        /// </summary>
        public List<Catalog> Services
        {
            get
            {
                return _services;
            }
        }
        #endregion Public Properties

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="catalog"></param>
        public void AddCatalog(Catalog catalog)
        {
            _services.Add(catalog);
        }
        #endregion Methods
    }
}
