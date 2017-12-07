using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Interface for Service Catalog operations
    /// </summary>
    public interface IServiceCatalogProvider
    {
        
        //Catalog GetServiceCatalog();

        //Task<Catalog> GetServiceCatalogAsync();

        //string GetServiceCatalogSimple();

        //Task<string> GetServiceCatalogsimpleAsync();

        /// <summary>
        /// Provides service catalog. Interface implementors can provide Catalog info in any format and from any source
        /// </summary>
        /// <returns></returns>
        Task<ServiceCatalogResponse> GetServiceCatalog();
    }
}
