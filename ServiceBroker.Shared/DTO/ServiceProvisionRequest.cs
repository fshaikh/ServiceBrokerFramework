using Newtonsoft.Json;
using System.Collections.Generic;

namespace ServiceBroker.Shared
{
    public enum ProvisionOpType
    {
        Provision =1,
        Deprovision = 2,
        LastOperation = 3
    }
    /// <summary>
    /// Represents Service Provision/Deprovision Request
    /// </summary>
    public class ServiceProvisionRequest
    {
        #region Members
        private IServiceProvisionParameterValidator _parametersValidator;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="ServiceProvisionRequest"/>
        /// </summary>
        public ServiceProvisionRequest()
        {
            // Do nothing
        }
        #endregion  Constructors
        #region Public Properties
        
        public IServiceProvisionParameterValidator ParameterValidator
        {
            get
            {
                return _parametersValidator;
            }
            set
            {
                _parametersValidator = value;
            }
        }

        public string InstanceId { get; set; }

        public bool AcceptsIncomplete { get; set; }

        /// <summary>
        /// The ID of the service (from the catalog). MUST be globally unique. MUST be a non-empty string.
        /// </summary>
        [JsonProperty("service_id")]
        public string ServiceId { get; set; }

        /// <summary>
        /// The ID of the plan (from the catalog) for which the service instance has been requested.
        /// MUST be unique to a service. MUST be a non-empty string.
        /// </summary>
        [JsonProperty("plan_id")]
        public string PlanId { get; set; }

        /// <summary>
        /// Platform specific contextual information under which the service instance is to be provisioned.
        /// Although most brokers will not use this field, it could be helpful in determining data placement or
        /// applying custom business rules. context will replace organization_guid and space_guid in future versions
        /// of the specification - in the interim both SHOULD be used to ensure interoperability
        /// with old and new implementations.
        /// </summary>
        [JsonProperty("context")]
        public object Context { get; set; }

        /// <summary>
        /// The platform GUID for the organization under which the service instance is to be provisioned.
        /// Although most brokers will not use this field, it might be helpful for executing operations on a user's behalf.
        /// MUST be a non-empty string.
        /// </summary>
        [JsonProperty("organization_guid")]
        public string OrganizationId { get; set; }

        /// <summary>
        /// The identifier for the project space within the platform organization. Although most brokers will not use
        /// this field, it might be helpful for executing operations on a user's behalf. MUST be a non-empty string.
        /// </summary>
        [JsonProperty("space_guid")]
        public string SpaceId { get; set; }

        /// <summary>
        /// Configuration options for the service instance. Controller treats this as a blob. Brokers SHOULD ensure
        /// that the client has provided valid configuration parameters and values for the operation.
        /// </summary>
        [JsonProperty("parameters")]
        public object Parameters { get; set; }

        /// <summary>
        /// A broker-provided identifier for the operation. When a value for operation is included with asynchronous responses
        /// for Provision, Update, and Deprovision requests
        /// </summary>
        [JsonProperty("operation",NullValueHandling = NullValueHandling.Ignore)]
        public string OperationText { get; set; }
        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// Validates the provision/deprovision request
        /// </summary>
        ///<param name="isDeprovision">False if provision request else true for deprovision request</param>
        /// <returns></returns>
        public List<ResponseBase> IsValid(ProvisionOpType opType)
        {
            List<ResponseBase> responses = new List<ResponseBase>();
            switch(opType)
            {
                case ProvisionOpType.Provision:
                    ValidateProvisionRequest(responses);
                    break;
                case ProvisionOpType.Deprovision:
                    ValidateDeprovisionRequest(responses);
                    break;
                case ProvisionOpType.LastOperation:
                    ValidateLastOperationRequest(responses);
                    break;
            }
            return responses;
        }


        #endregion Public Methods

        #region Helper Methods
        private void ValidateProvisionRequest(List<ResponseBase> responses)
        {
            if (string.IsNullOrEmpty(InstanceId))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Description is empty"
                });
            }

            if (string.IsNullOrEmpty(ServiceId))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Service Id is empty"
                });
            }

            if (string.IsNullOrEmpty(PlanId))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Plan Id is empty"
                });
            }

            if (string.IsNullOrEmpty(OrganizationId))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Organization Id is empty"
                });
            }

            if (string.IsNullOrEmpty(SpaceId))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Space Id is empty"
                });
            }
            responses.AddRange(IsParametersValid());
        }

        private void ValidateDeprovisionRequest(List<ResponseBase> responses)
        {
            if (string.IsNullOrEmpty(InstanceId))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Instance Id is empty"
                });
            }

            if (string.IsNullOrEmpty(ServiceId))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Service Id is empty"
                });
            }

            if (string.IsNullOrEmpty(PlanId))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Plan Id is empty"
                });
            }
        }

        private void ValidateLastOperationRequest(List<ResponseBase> responses)
        {
            if (string.IsNullOrEmpty(InstanceId))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Instance Id is empty"
                });
            }

            if (string.IsNullOrEmpty(ServiceId))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Service Id is empty"
                });
            }

            if (string.IsNullOrEmpty(PlanId))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Plan Id is empty"
                });
            }

            if (string.IsNullOrEmpty(OperationText))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Operation Text is empty"
                });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<ResponseBase> IsParametersValid()
        {
            List<ResponseBase> responses = new List<ResponseBase>();
            if(_parametersValidator == null)
            {
                return responses;
            }
            return _parametersValidator.IsValid(this);
        }
        #endregion Helper Methods
    }
}
