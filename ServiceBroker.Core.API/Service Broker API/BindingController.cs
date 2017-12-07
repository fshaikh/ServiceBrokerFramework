using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceBroker.Shared;
using System.IO;
using System;

namespace ServiceBroker.Core.API
{
    /// <summary>
    /// API Controller for Binding/Unbinding Service Broker API
    /// </summary>
    public class BindingController : ApiControllerBase
    {
        #region Constants
        private const string ACCEPTS_INCOMPLETE_KEY = "accepts_incomplete";
        #endregion Constants

        #region Members
        private IServiceBindingProvider _serviceBindingProvider;
        private IServiceBindParameterValidator _parametersValidator;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="BindingController"/>
        /// </summary>
        /// <param name="serviceBindingProvider">Service Binding Provider</param>
        public BindingController(IServiceBindingProvider serviceBindingProvider, IServiceBindParameterValidator parametersValidator)
        {
            _serviceBindingProvider = serviceBindingProvider;
            _parametersValidator = parametersValidator;
        }
        #endregion Constructors

        #region Service Broker Binding/Unbinding API
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
        [Route("/v2/service_instances/{instance_id}/service_bindings/{binding_id}")]
        public async virtual Task<IActionResult> BindServiceInstance(string instance_id,string binding_id)
        {
            ResponseBase response;
            // 1. Construct the ServiceBindRequest from various input sources
            ServiceBindRequest request = await BuildBindRequest(instance_id, binding_id);
            // 2. Validate the request
            //      2.a If validation fails, return
            bool isValid = request.IsValid();
            if (!isValid)
            {
                response = new ResponseBase
                {
                    Description = "Bad Request. Check required fields or parameters missing"
                };
                return BadRequest(response);
            }
            // 3. If provider is not set, send 500 error with ServiceProvisionResponse
            if (_serviceBindingProvider == null)
            {
                response = new ResponseBase
                {
                    Description = "Internal server error. Service Binding Provider is not set"
                };
                return ServerError(response);
            }
            // 4. Invoke the Provider
            response = await this._serviceBindingProvider.BindServiceInstance(request);

            // 5. Capture the response and send
            return GetControllerResponse(response);
        }

        [HttpDelete]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Route("/v2/service_instances/{instance_id}/service_bindings/{binding_id}")]
        public virtual async Task<IActionResult> UnbindServiceInstance(string instance_id,string binding_id)
        {
            ResponseBase response;
            // 1. Construct the ServiceBindRequest from various input sources
            ServiceBindRequest request = BuildUnBindRequest(instance_id, binding_id);
            // 2. Validate the request
            //      2.a If validation fails, return
            bool isValid = request.IsValid();
            if (!isValid)
            {
                response = new ResponseBase
                {
                    Description = "Bad Request. Check required fields or parameters missing"
                };
                return BadRequest(response);
            }
            // 3. If provider is not set, send 500 error with ServiceProvisionResponse
            if (_serviceBindingProvider == null)
            {
                response = new ResponseBase
                {
                    Description = "Internal server error. Service Binding Provider is not set"
                };
                return ServerError(response);
            }
            // 4. Invoke the Provider
            response = await this._serviceBindingProvider.UnbindServiceInstance(request);

            // 5. Capture the response and send
            return GetControllerResponse(response);
        }
        #endregion Service Broker Binding/Unbinding API

        #region Helper Methods

        /// <summary>
        /// Builds ServiceProvisionRequest from various input sources. 
        /// </summary>
        /// <param name="instance_id"></param>
        /// <param name="binding_id"></param>
        /// <returns></returns>
        protected async Task<ServiceBindRequest> BuildBindRequest(string instance_id,string binding_id)
        {
            ServiceBindRequest request = await SetProperties();
            request.InstanceId = instance_id;
            request.BindingId = binding_id;
            request.ParameterValidator = this._parametersValidator;
            return request;
        }

        /// <summary>
        /// Builds request from various input sources. 
        /// </summary>
        /// <param name="instance_id"></param>
        /// <param name="binding_id"></param>
        /// <returns></returns>
        protected ServiceBindRequest BuildUnBindRequest(string instance_id, string binding_id)
        {
            ServiceBindRequest request = new ServiceBindRequest
            {
                InstanceId = instance_id,
                BindingId = binding_id,
                ServiceId = (string)this.GetValueFromQS(SERVICE_ID_KEY),
                PlanId = (string)this.GetValueFromQS(PLAN_ID_KEY)
            };
            return request;
        }

        protected async Task<ServiceBindRequest> SetProperties()
        {
            return await this.GetTypedRequestFromBody<ServiceBindRequest>();
        }
        #endregion Helper Methods
    }
}
