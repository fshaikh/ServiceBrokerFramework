using System.Collections.Generic;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Represents a service plan
    /// </summary>
    public class Plan: DTOBase
    {
        #region Members
        private bool _free = true;
        private bool _bindable = false;
        private PlanMetadata _metadata;
        #endregion Members

        #region Public Properties
        /// <summary>
        /// A short description of the plan. MUST be a non-empty string.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// An opaque object of metadata for a service plan. Controller treats this as a blob.
        /// Note that there are conventions in existing brokers and controllers for fields that aid in the
        /// display of catalog data.
        /// </summary>
        public PlanMetadata Metadata
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
        /// When false, service instances of this plan have a cost. The default is true.
        /// </summary>
        public bool Free
        {
            get
            {
                return _free;
            }
            set
            {
                _free = value;
            }
        }

        /// <summary>
        /// Specifies whether service instances of the service can be bound to applications.
        /// This specifies the default for all plans of this service. Plans can override this field
        /// </summary>
        public bool Bindable
        {
            get
            {
                return _bindable;
            }
            set
            {
                _bindable = value;
            }
        }

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
            if (string.IsNullOrEmpty(this.Description))
            {
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Description is empty"
                });
            }


            return responses;
        }
        #endregion Methods
    }
}
