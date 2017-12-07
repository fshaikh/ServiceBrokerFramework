using Newtonsoft.Json;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Represents Service Bind/Unbind Request
    /// </summary>
    public class ServiceBindRequest
    {
        #region Members
        private IServiceBindParameterValidator _parametersValidator;
        #endregion Members

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="ServiceBindRequest"/>
        /// </summary>
        /// <param name="parametersValidator">Parameters Validator</param>
        public ServiceBindRequest()
        {
            // Do nothing
        }
        #endregion  Constructors

        #region Public Properties

        public IServiceBindParameterValidator ParameterValidator
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

        /// <summary>
        /// Gets/Sets ID of a previously provisioned service instance
        /// </summary>
        public string InstanceId { get; set; }

        /// <summary>
        /// Gets/Sets ID used for future unbind requests, so the broker will use it to correlate
        /// the resource it creates. Provided by the Cloud Foundry platform
        /// </summary>
        public string BindingId { get; set; }

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
        /// A JSON object that contains data for platform resources associated with the binding to be created.
        /// </summary>
        [JsonProperty("bind_resource")]
        public BindResource Resource { get; set; }


        /// <summary>
        /// Configuration options for the service instance. Controller treats this as a blob. Brokers SHOULD ensure
        /// that the client has provided valid configuration parameters and values for the operation.
        /// </summary>
        [JsonProperty("parameters")]
        public object Parameters { get; set; }
        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// Validates the bind/unbind request
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            bool isValid = true;
            isValid = !string.IsNullOrEmpty(InstanceId) ||
                      !string.IsNullOrEmpty(BindingId) ||
                     !string.IsNullOrEmpty(ServiceId) ||
                     !string.IsNullOrEmpty(PlanId) ||
                     !IsParametersValid();
            return isValid;
        }


        #endregion Public Methods

        #region Helper Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsParametersValid()
        {
            if (_parametersValidator == null)
            {
                return true;
            }
            return _parametersValidator.IsValid(this);
        }
        #endregion Helper Methods
    }
}
