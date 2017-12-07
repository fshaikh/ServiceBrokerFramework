using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceBroker.Shared;
using System.Collections.Generic;

namespace ServiceBroker.Core.API
{
    /// <summary>
    /// API Controller for Service Broker Catalog API
    /// </summary>
    public class CatalogController : ApiControllerBase
    {
        #region Members
        private IServiceCatalogProvider _catalogProvider;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="CatalogController"/>
        /// </summary>
        /// <param name="catalogProvider">Service Catalog Provider</param>
        public CatalogController(IServiceCatalogProvider catalogProvider)
        {
            _catalogProvider = catalogProvider;
        }
        #endregion Constructors

        #region Service Broker Catalog API
        /// <summary>
        /// Implements Service Broker Catalog API to fetch the service catalog
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [Route("v2/catalog")]
        public async virtual Task<IActionResult> GetServiceCatalog()
        {
            ServiceCatalogResponse response = await _catalogProvider.GetServiceCatalog();
            if(!response.IsSuccess)
            {
                Console.WriteLine("Invalid Catalog Data sent by Provider");
                return ServerError(response);
            }
            return SendCatalog(response.CatalogData);
        }

        #endregion Service Broker Catalog API

        #region Helper Methods
        private IActionResult SendCatalog(object data)
        {
            switch(data.GetType().ToString())
            {
                case "System.String":
                    return Content((string)data);
                case "ServiceBroker.Shared.ServiceCatalogs":
                    return HandleCatalogResponse(data);
                default:
                    return GetCatalogErrorResponse();
            }
        }

        private IActionResult HandleCatalogResponse(object data)
        {
            // Check if we have a ServiceCatalogs object
            ServiceCatalogs catalogs = data as ServiceCatalogs;
            if(catalogs == null)
            {
                return GetCatalogErrorResponse();
            }
            // Check if the data is valid
            foreach (var item in catalogs.Services)
            {
                List<ResponseBase> responses = item.IsValid();
                if(responses.Count > 0)
                {
                    string errorMessage = responses.ToMessageString();
                    Console.WriteLine("Invalid Catalog data : " + errorMessage);
                    return GetCatalogErrorResponse(errorMessage);
                }
            }

            // Send the json response
            return Json((ServiceCatalogs)data);

        }

        private IActionResult GetCatalogErrorResponse(string description = "Server error in parsing catalog data")
        {
            ResponseBase response = new ResponseBase
            {
                IsSuccess = false,
                Description = description
            };
            Console.WriteLine(response.Description);
            return ServerError(response);
        }
        #endregion Helper Methods
    }
}
