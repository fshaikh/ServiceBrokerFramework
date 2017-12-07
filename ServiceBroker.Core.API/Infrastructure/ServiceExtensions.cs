using Microsoft.Extensions.DependencyInjection;
using ServiceBroker.Shared;
using System;
using ServiceBroker.Middleware.AuthProvider;


namespace ServiceBroker.Core.API
{
    /// <summary>
    /// This calls defines extension methods for registering services
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Registers FileBasedCatalogProvider to the services collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddFileBasedCatalogProvider(this IServiceCollection services,CatalogOptions options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            services.Add(new ServiceDescriptor(typeof(IServiceCatalogProvider), FileBasedCatalogProvider.Create(options)));
            return services;
        }

        /// <summary>
        /// Registers BasicAuthenticationProvider to the services collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="credentialsProvider"></param>
        /// <returns></returns>
        public static IServiceCollection AddBasicAuthenticationProvider(this IServiceCollection services)
        {
            if(services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            var credentialsProvider = services.BuildServiceProvider().GetRequiredService<ICredentialsProvider>();
            services.Add(new ServiceDescriptor(typeof(IAuthenticationProvider), BasicAuthenticationProvider.Create(credentialsProvider)));
            return services;
        }

        /// <summary>
        /// Registers EnvironmentCredentialsProvider to the services collection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEnvironmentCredentialsProvider(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.Add(new ServiceDescriptor(typeof(ICredentialsProvider), new EnvironmentCredentialsProvider()));
            return services;
        }
    }
}
