using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceBroker.Shared;
using System;
using System.Collections.Generic;

namespace ServiceBroker.Core.API
{
    /// <summary>
    /// API Controller for Provisioning/Deprovisioning Service Broker API
    /// </summary>
    public class ProvisioningController : ApiControllerBase
    {
        #region Constants
        private const string ACCEPTS_INCOMPLETE_KEY = "accepts_incomplete";
        private const string OPERATION_KEY = "operation";
        #endregion Constants

        #region Members
        private IServiceProvisionProvider _provisionProvider;
        private IServiceProvisionParameterValidator _parametersValidator;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="ProvisioningController"/>
        /// </summary>
        /// <param name="provisionProvider">Service Provisioning Provider</param>
        public ProvisioningController(IServiceProvisionProvider provisionProvider,IServiceProvisionParameterValidator parametersValidator)
        {
            _provisionProvider = provisionProvider;
            _parametersValidator = parametersValidator;
        }
        #endregion Constructors

        #region Service Broker Provisioning/Deprovisioning API
        /// <summary>
        ///  API method for implementing Service Broker Provision API. Invokes <see cref="IServiceProvisionProvider"/> for actual provisioning
        /// </summary>
        /// <param name="instance_id">Service instance Id provided by the platform. This ID will be used
        /// for future requests (bind and deprovision), so the broker will use it to correlate the
        /// resource it creates.</param>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("v2/service_instances/{instance_id}")]
        public async virtual Task<IActionResult> ProvisionService(string instance_id)
        {
            ServiceProvisionResponse response;
            // 1. Construct the ServiceProvisionRequest from various input sources
            ServiceProvisionRequest request = await BuildProvisionRequest(instance_id);

            // 2. Validate the request
            //      2.a If validation fails, return
            List<ResponseBase> responses = request.IsValid(ProvisionOpType.Provision);
            if(responses.Count > 0)
            {
                response = new ServiceProvisionResponse
                {
                    Description = responses.ToMessageString()
                };
                return ServerError(response);
            }

            // 3. If provider is not set, send 500 error with ServiceProvisionResponse
            if(_provisionProvider == null)
            {
                string description = "Internal server error. Service Provision Provider is not set";
                Console.WriteLine(description);
                response = new ServiceProvisionResponse
                {
                    Description = description
                };
                return ServerError(response);
            }

            // 4. Check the incomplete request support
            // From the SPEC:
            // For a broker to return an asynchronous response, the query parameter accepts_incomplete=true MUST be included the request.
            // If the parameter is not included or is set to false, and the broker cannot fulfill the request synchronously
            // (guaranteeing that the operation is complete on response), then the broker SHOULD reject the request with the
            // status code 422 Unprocessable Entit
            if(this._provisionProvider.SupportAsynchronousRequest(request) && !request.AcceptsIncomplete)
            {
                response = new ServiceProvisionResponse
                {
                    Error = "AsyncRequired",
                    Description = "This service plan requires client support for asynchronous service operations."
                };
                return UnprocessableEntity(response);
            }

            // 5. Invoke the Provider
            response = this._provisionProvider.ProvisionService(request);

            // 6. Capture the response and send
            return GetControllerResponse(response);
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance_id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("v2/service_instances/{instance_id}")]
        public async virtual Task<IActionResult> DeprovisionService(string instance_id)
        {
            ServiceProvisionResponse response;
            // 1. Construct the ServiceProvisionRequest from various input sources
            ServiceProvisionRequest request = BuildDeprovisionRequest(instance_id);
            // 2. Validate the request
            //      2.a If validation fails, return
            List<ResponseBase> responses = request.IsValid(ProvisionOpType.Deprovision);
            if (responses.Count > 0)
            {
                response = new ServiceProvisionResponse
                {
                    Description = responses.ToMessageString()
                };
                return ServerError(response);
            }
            // 3. If provider is not set, send 500 error with ServiceProvisionResponse
            if (_provisionProvider == null)
            {
                response = new ServiceProvisionResponse
                {
                    Description = "Internal server error. Service Provision Provider is not set"
                };
                return ServerError(response);
            }

            // 4. Check the incomplete request support
            // From the SPEC:
            // For a broker to return an asynchronous response, the query parameter accepts_incomplete=true MUST be included the request.
            // If the parameter is not included or is set to false, and the broker cannot fulfill the request synchronously
            // (guaranteeing that the operation is complete on response), then the broker SHOULD reject the request with the
            // status code 422 Unprocessable Entit
            if (this._provisionProvider.SupportAsynchronousRequest(request) && !request.AcceptsIncomplete)
            {
                response = new ServiceProvisionResponse
                {
                    Error = "AsyncRequired",
                    Description = "This service plan requires client support for asynchronous service operations."
                };
                return UnprocessableEntity(response);
            }

            // 4. Invoke the Provider
            response = this._provisionProvider.DeprovisionService(request);

            // 5. Capture the response and send
            return GetControllerResponse(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("v2/service_instances/{instance_id}/last_operation")]
        public async virtual Task<IActionResult> GetLastOperation(string instance_id)
        {
            LastOperationProvisionResponse response;
            // 1. Construct the ServiceProvisionRequest from various input sources
            ServiceProvisionRequest request = BuildLastOperationRequest(instance_id);

            // 2. Validate the request
            //      2.a If validation fails, return
            List<ResponseBase> responses = request.IsValid(ProvisionOpType.LastOperation);
            if (responses.Count > 0)
            {
                response = new LastOperationProvisionResponse
                {
                    Description = responses.ToMessageString()
                };
                return ServerError(response);
            }

            // 3. If provider is not set, send 500 error with ServiceProvisionResponse
            if (_provisionProvider == null)
            {
                string description = "Internal server error. Service Provision Provider is not set";
                Console.WriteLine(description);
                response = new LastOperationProvisionResponse
                {
                    Description = description
                };
                return ServerError(response);
            }

            // 4. Check the incomplete request support
            // From the SPEC:
            // For a broker to return an asynchronous response, the query parameter accepts_incomplete=true MUST be included the request.
            // If the parameter is not included or is set to false, and the broker cannot fulfill the request synchronously
            // (guaranteeing that the operation is complete on response), then the broker SHOULD reject the request with the
            // status code 422 Unprocessable Entit
            if (this._provisionProvider.SupportAsynchronousRequest(request) && !request.AcceptsIncomplete)
            {
                response = new LastOperationProvisionResponse
                {
                    Error = "AsyncRequired",
                    Description = "This service plan requires client support for asynchronous service operations."
                };
                return UnprocessableEntity(response);
            }

            // 5. Invoke the Provider
            response = this._provisionProvider.GetLastOperationResponse(request);

            // 6. Capture the response and send
            return GetLastOperationResponse(response);
        }

        #endregion Service Broker Provisioning/Deprovisioning API

        #region Helper Methods
        /// <summary>
        /// Builds ServiceProvisionRequest from various input sources
        /// </summary>
        /// <param name="instance_id"></param>
        /// <returns></returns>
        private async Task<ServiceProvisionRequest> BuildProvisionRequest(string instance_id)
        {
            ServiceProvisionRequest request = await SetProperties();
            request.InstanceId = instance_id;
            request.AcceptsIncomplete = GetAcceptsIncompleteValue();
            request.ParameterValidator = this._parametersValidator;
            return request;
        }

        /// <summary>
        /// Builds ServiceProvisionRequest from various input sources. 
        /// </summary>
        /// <param name="instance_id"></param>
        /// <returns></returns>
        private ServiceProvisionRequest BuildDeprovisionRequest(string instance_id)
        {
            ServiceProvisionRequest request = new ServiceProvisionRequest
            {
                InstanceId = instance_id,
                AcceptsIncomplete = GetAcceptsIncompleteValue(),
                ServiceId = (string)GetValueFromQS(SERVICE_ID_KEY),
                PlanId = (string)GetValueFromQS(PLAN_ID_KEY)
            };

            return request;
        }

        /// <summary>
        /// Builds ServiceProvisionRequest for Last Operation request from the platform
        /// </summary>
        /// <param name="instance_id"></param>
        /// <returns></returns>
        private ServiceProvisionRequest BuildLastOperationRequest(string instance_id)
        {
            ServiceProvisionRequest request = new ServiceProvisionRequest
            {
                InstanceId = instance_id,
                ServiceId = (string)GetValueFromQS(SERVICE_ID_KEY),
                PlanId = (string)GetValueFromQS(PLAN_ID_KEY),
                OperationText = (string)GetValueFromQS(OPERATION_KEY)
            };

            return request;
        }

        /// <summary>
        /// Reads accepts_incomplete parameter value from query string
        /// </summary>
        /// <returns></returns>
        private bool GetAcceptsIncompleteValue()
        {
            bool value = false;
            string qsValue = (string)GetValueFromQS(ACCEPTS_INCOMPLETE_KEY);
            bool status = bool.TryParse(qsValue, out value);
            return status ? value : false;
        }

        private async Task<ServiceProvisionRequest> SetProperties()
        {
            return await this.GetTypedRequestFromBody<ServiceProvisionRequest>();
        }

        /// <summary>
        

        /// <summary>
        /// Gets the action result for the last operation request. Modifies response object based on the spec.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private IActionResult GetLastOperationResponse(LastOperationProvisionResponse response)
        {
            IActionResult actionResult;
            switch(response.ResponseCode)
            {
                case LastOperationResponseCode.Success:
                    actionResult = Ok(response);
                    break;
                case LastOperationResponseCode.Gone:
                    actionResult = Gone(response);
                    break;
                default:
                    actionResult = ServerError(response);
                    break;
            }
            return actionResult;
        }
        #endregion Helper Methods
    }
}
