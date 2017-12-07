using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Provides Catalog data by reading from environment variable
    /// </summary>
    public class EnvironmentBasedCatalogProvider : IServiceCatalogProvider
    {
        #region Constants
        private const string ENV_KEY = "CatalogData";
        #endregion Constants

        #region Members
        private IConfiguration _configuration;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="EnvironmentBasedCatalogProvider"/>
        /// </summary>
        /// <param name="configuration">Configuration object to read config data</param>
        public EnvironmentBasedCatalogProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion Constructors

        #region IServiceCatalogProvider Methods
        /// <summary>
        /// Reads Catalog data from CF envrionment variable. Usage:
        /// 1. set-env <SERVICEBROKER_NAME> CatalogData {JSON Blob}
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceCatalogResponse> GetServiceCatalog()
        {
            ServiceCatalogResponse response = new ServiceCatalogResponse();
            // Read the environment variable from CF
            string catalogData = Environment.GetEnvironmentVariable(ENV_KEY);
            if (string.IsNullOrEmpty(catalogData))
            {
                response.IsSuccess = false;
                response.Description = "Either CatalogData environment variable not set or empty";
                Console.WriteLine(response.Description);
            }
            response.CatalogData = catalogData;
            return response;
        }
        #endregion IServiceCatalogProvider Methods
    }
}
