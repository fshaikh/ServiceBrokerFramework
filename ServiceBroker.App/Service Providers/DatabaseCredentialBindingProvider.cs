using ServiceBroker.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBroker.App
{
    public class DatabaseCredentialBindingProvider : CredentialsBindingProviderBase
    {
        public async override Task<ResponseBase> BindServiceInstance(ServiceBindRequest request)
        {
            ResponseBase response = await base.BindServiceInstance(request);
            if (!response.IsSuccess)
            {
                return response;
            }
            CredentialsBindingResponse credentialsResponse = new CredentialsBindingResponse
            {
                IsSuccess = true,
                ResponseCode = ResponseCode.SuccessfulProvision,
                Credentials = new Credentials
                {
                    Uri = "mysql://mysqluser:pass@mysqlhost:3306/dbname",
                    Username = "mysqluser",
                    Password = "pass",
                    Host = "mysqlhost",
                    Port = 3306,
                    BackingStoreName = "dbname"
                }
            };
            return credentialsResponse;
        }

        public async override Task<ResponseBase> UnbindServiceInstance(ServiceBindRequest request)
        {
            Console.WriteLine(string.Format("Unbind service instance. Instance Id:{0}, binding Id : {1}",request.InstanceId,request.BindingId));
            CredentialsBindingResponse credentialsResponse = new CredentialsBindingResponse
            {
                IsSuccess = true,
                ResponseCode = ResponseCode.SuccessfulDelete
            };
            return credentialsResponse;
        }
    }
}
