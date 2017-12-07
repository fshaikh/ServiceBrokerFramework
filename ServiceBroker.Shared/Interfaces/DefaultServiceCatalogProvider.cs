//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace ServiceBroker.Shared
//{
//    /// <summary>
//    /// Default Catalog Provider. Reads Catalog data from in-memory JSON file
//    /// </summary>
//    public class DefaultServiceCatalogProvider : IServiceCatalogProvider
//    {
//        #region IServiceCatalogProvider Methods
//        public Catalog GetServiceCatalog()
//        {
//            throw new NotImplementedException();
            
//        }

//        public async Task<Catalog> GetServiceCatalogAsync()
//        {
//            throw new NotImplementedException();
//        }

//        public string GetServiceCatalogSimple()
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<string> GetServiceCatalogsimpleAsync()
//        {
//            string catalogJson = await AssemblyExtensions.GetStringEmbeddedResource("ServiceBroker.Shared.defaultCatalog.json");
//            return catalogJson;
//        }
//        #endregion IServiceCatalogProvider Methods
//    }
//}
