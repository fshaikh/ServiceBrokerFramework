using ServiceBroker.Shared;
using System;


namespace ServiceBroker.App
{
    /// <summary>
    /// Sample implementation for a service provision provider
    /// </summary>
    public class ServiceProvisionProvider : IServiceProvisionProvider
    {
        public ServiceProvisionResponse ProvisionService(ServiceProvisionRequest request)
        {
            Console.WriteLine(string.Format("Provisioning Service: {0} Plan: {1} Org:{2} Space: {3}", request.ServiceId, request.PlanId, request.OrganizationId, request.SpaceId));
            return new ServiceProvisionResponse
            {
                DashboardUrl = "https://www.google.com",
                Operation ="Done",
                ResponseCode = ResponseCode.ProvisioningInProgress
            };
        }

        public ServiceProvisionResponse DeprovisionService(ServiceProvisionRequest request)
        {
            Console.WriteLine(string.Format("DeProvisioning Service: {0} Plan: {1} Org:{2} Space: {3}", request.ServiceId, request.PlanId, request.OrganizationId, request.SpaceId));
            return new ServiceProvisionResponse
            {
                Operation = "Done"
            };
        }

        public bool SupportAsynchronousRequest(ServiceProvisionRequest request)
        {
            return false;
        }

        public LastOperationProvisionResponse GetLastOperationResponse(ServiceProvisionRequest request)
        {
            Console.WriteLine(string.Format("GetLastOperationResponse: {0}", request.InstanceId) );
            return new LastOperationProvisionResponse
            {
                IsSuccess = true,
                ResponseCode = LastOperationResponseCode.Success,
                State = Environment.GetEnvironmentVariable("Stop") == "1" ? LastOperationStateCode.Succeded : LastOperationStateCode.InProgress
            };
        }
    }
}
