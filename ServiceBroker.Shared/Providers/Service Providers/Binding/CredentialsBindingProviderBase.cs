using System;
using System.Threading.Tasks;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Base class for Credentails Binding Provider. Implementers can derive from this class and provide custom logic.
    /// This class provides common functionality : Validation, etc
    /// </summary>
    public abstract class CredentialsBindingProviderBase : IServiceBindingProvider
    {
        public async virtual Task<ResponseBase> BindServiceInstance(ServiceBindRequest request)
        {
            CredentialsBindingResponse response = new CredentialsBindingResponse();

            // 1. Validate the request. Must contain application guid in the request sent by the platform
            if(string.IsNullOrEmpty(request.Resource.AppGuid))
            {
                response = new CredentialsBindingResponse
                {
                    Error = "RequiresApp",
                    IsSuccess = false,
                    Description = "This service supports generation of credentials through binding an application only.",
                    ResponseCode = ResponseCode.UnprocessableEntity

                };
                return response;
            }
            return response;
        }

        public abstract Task<ResponseBase> UnbindServiceInstance(ServiceBindRequest request);
        
    }
}
