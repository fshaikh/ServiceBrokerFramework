using System;
using System.IO;
using System.Threading.Tasks;

namespace ServiceBroker.Shared
{
    public class FileBasedCatalogProvider : IServiceCatalogProvider
    {
        #region Public Properties
        /// <summary>
        /// Gets/Sets the file path to read the catalog json from
        /// </summary>
        public CatalogOptions  Options{ get; set; }

        #endregion Public Properties

        public async Task<ServiceCatalogResponse> GetServiceCatalog()
        {
            ServiceCatalogResponse response = new ServiceCatalogResponse();

            if (string.IsNullOrEmpty(Options.File) || !(File.Exists(Options.File)))
            {
                response.IsSuccess = false;
                response.Description = string.Format("Invalid Catalog File Path : {0}", string.IsNullOrEmpty(Options.File)? "" : Options.File );
                Console.WriteLine("Invalid Catalog File Path");
                return response;
            }

            response.CatalogData = await File.ReadAllTextAsync(Options.File);
            return response; 
        }

        public static FileBasedCatalogProvider Create(CatalogOptions options)
        {
            return new FileBasedCatalogProvider
            {
                Options = options
            };
        }
    }
}
