using System;
using System.Threading.Tasks;

namespace ServiceBroker.Shared
{
    public class SampleCatalogProvider : IServiceCatalogProvider
    {
        public async Task<ServiceCatalogResponse> GetServiceCatalog()
        {
            ServiceCatalogResponse response = new ServiceCatalogResponse();

            ServiceCatalogs services = new ServiceCatalogs();
            Catalog catalog = new Catalog();
            catalog.Bindable = true;
            catalog.DashboardClient = null;
            catalog.Description = "Sample Service Broker";
            catalog.Id = Guid.NewGuid();
            catalog.Metadata = new ServiceMetadata
            {
                DisplayName = "Sample Service Broker",
                DocumentationUrl = "http://google.com",
                ImageUrl = "http://example.com/image.png",
                LongDescription = "Sample Service Broker built using a cool framework",
                ProviderDisplayName = "FPService",
                SupportUrl = "https://dell.com"
            };
            catalog.Name = "FP Service";
            catalog.Plans = new System.Collections.Generic.List<Plan>
            {
                new Plan
                {
                    Id = Guid.NewGuid(),
                    Name = "Standard Plan",
                    Bindable = true,
                    Description = "Standard Plan includes 30 days of free service",
                    Free = true,
                    Metadata = new PlanMetadata
                    {
                        DisplayName = "Standard Plan",
                        Bullets = new System.Collections.Generic.List<string>
                        {
                            "30 days free trial",
                            "No credit card required",
                            "Awesome Plan"
                        },
                        Costs = new System.Collections.Generic.List<CostMetadata>
                        {
                            new CostMetadata
                            {
                                Unit = CostUnitEnum.Monthly,
                                Amount = new AmountMetadata
                                {
                                    CurrencyCode = "USD",
                                    Value = 0
                                }
                            }
                        }
                    }
                }
            };
            catalog.PlanUpdateable = false;
            catalog.Tags = new string[]
            {
                "NoSql",
                "Distributed"
            };
            services.AddCatalog(catalog);

            response.CatalogData = services;
            return response;
        }
    }
}
