using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBroker.App;
using ServiceBroker.Shared;
using ServiceBroker.Middleware.HeaderVerifier;
using ServiceBroker.Middleware.AuthProvider;

namespace ServiceBroker.Core.API
{
    /// <summary>
    /// Startup class for registering services and configuring HTTP request pipeline
    /// </summary>
    public class Startup
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="Startup"/>
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion Constructors

        #region Public Properties
        /// <summary>
        /// Gets/Sets the Configuration Service
        /// </summary>
        public IConfiguration Configuration { get; }
        #endregion Public Properties

        #region Methods
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Add Service for Service Broker Providers
            //services.AddSingleton<IServiceCatalogProvider, SampleCatalogProvider>();
            //services.AddFileBasedCatalogProvider(new CatalogOptions
            //{
            //    File = Environment.GetEnvironmentVariable("CatalogFile")
            //});
            services.AddSingleton<IServiceCatalogProvider, SampleCatalogProvider>();
            services.AddTransient<IServiceProvisionProvider, ServiceProvisionProvider>();
            services.AddTransient<IServiceProvisionParameterValidator, ServiceProvisionParameterValidator>();
            services.AddTransient<IServiceBindingProvider, DatabaseCredentialBindingProvider>();
            services.AddTransient<IServiceBindParameterValidator, ServiceBindParameterValidator>();
            services.AddEnvironmentCredentialsProvider();
            services.AddBasicAuthenticationProvider();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseServiceBrokerAuthentication();
            app.UseHeaderVerifier();

            app.UseMvc();
        }
        #endregion Methods
    }
}
