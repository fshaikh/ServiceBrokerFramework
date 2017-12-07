using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Represents Service Catalog
    /// </summary>
    public class Catalog: DTOBase
    {
        #region Members
        private bool _planUpdateable = false;
        private ServiceMetadata _metadata;
        #endregion Members

        #region Public Properties
        /// <summary>
        /// A short description of the service. MUST be a non-empty string.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Tags provide a flexible mechanism to expose a classification, attribute, or base technology
        /// of a service, enabling equivalent services to be swapped out without changes to dependent logic
        /// in applications, buildpacks, or other services.
        /// E.g. mysql, relational, redis, key-value, caching, messaging, amqp.
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// A list of permissions that the user would have to give the service, if they provision it.
        /// The only permissions currently supported are syslog_drain, route_forwarding and volume_mount.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Requires { get; set; }

        /// <summary>
        /// Specifies whether service instances of the service can be bound to applications.
        /// This specifies the default for all plans of this service. Plans can override this field
        /// </summary>
        public bool Bindable { get; set; }

        /// <summary>
        /// An opaque object of metadata for a service offering. Controller treats this as a blob.
        /// Note that there are conventions in existing brokers and controllers for fields that aid in the
        /// display of catalog data.
        /// </summary>
        public ServiceMetadata Metadata
        {
            get
            {
                return _metadata;
            }
            set
            {
                _metadata = value;
            }
        }


        /// <summary>
        /// Contains the data necessary to activate the Dashboard SSO feature for this service.
        /// </summary>
        [JsonProperty("dashboard_client")]
        public DashboardClient DashboardClient { get; set; }

        /// <summary>
        /// Whether the service supports upgrade/downgrade for some plans
        /// Defaults to false.
        /// </summary>
        [JsonProperty("plan_updateable")]
        public bool PlanUpdateable
        {
            get
            {
                return _planUpdateable;
            }
            set
            {
                _planUpdateable = value;
            }
        }

        /// <summary>
        /// A list of plans for this service. MUST contain at least one plan.
        /// </summary>
        public List<Plan> Plans { get; set; }
        #endregion Public Properties

        #region Methods
        /// <summary>
        /// Validates the object based on rules
        /// </summary>
        /// <returns>Validation error messages</returns>
        public override List<ResponseBase> IsValid()
        {
            List<ResponseBase> responses = new List<ResponseBase>();
            responses.AddRange(base.IsValid());

            if(string.IsNullOrEmpty(this.Description))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Description is empty"
                });
            }

            if(!ServicePermissions.IsValid(this.Requires))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = string.Format("Invalid Permission Value")
                });
            }

            responses.AddRange(IsPlanValid());
                      
            return responses;
        }

        #region Helper Methods
        /// <summary>
        /// Validation for Service Plans
        /// </summary>
        /// <returns></returns>
        private List<ResponseBase> IsPlanValid()
        {
            List<ResponseBase> responses = new List<ResponseBase>();
            if (this.Plans == null || this.Plans.Count == 0)
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "No plans exist for the service"
                });
                return responses;
            }

            // iterate each plan and check for validity
            for (int i = 0; i < this.Plans.Count; i++)
            {
                responses.AddRange(this.Plans[i].IsValid());
            }
            return responses;
        }
        #endregion Helper Methods
        #endregion Methods
    }
}
