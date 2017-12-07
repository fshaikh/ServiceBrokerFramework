using System.Threading.Tasks;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Interface which defines methods for binding/unbinding service broker request
    /// </summary>
    public interface IServiceBindingProvider
    {
        /// <summary>
        /// Handles service bind request
        /// </summary>
        /// <param name="request">Request object</param>
        /// <returns>Response object</returns>
        Task<ResponseBase> BindServiceInstance(ServiceBindRequest request);

        /// <summary>
        /// Handles service unbind request
        /// </summary>
        /// <param name="request">Request object</param>
        /// <returns>Response object. Return </returns>
        Task<ResponseBase> UnbindServiceInstance(ServiceBindRequest request);
    }
}
