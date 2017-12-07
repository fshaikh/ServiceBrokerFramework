using System;
using ServiceBroker.Shared.Utilities;
using System.Collections.Generic;
using System.Text;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Base class for all DTO classes. Defines common properties
    /// </summary>
    public class DTOBase
    {
        #region Private Members
        private string _serviceName = string.Empty;
        #endregion Private Members

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="DTOBase"/>
        /// </summary>
        public DTOBase():this(new Guid())
        {
            // Do nothing
        }


        /// <summary>
        /// Initializes a new instance of <see cref="DTOBase"/>
        /// </summary>
        /// <param name="id">Id of the service</param>
        public DTOBase(Guid id)
        {
            this.Id = id;
        }
        #endregion Constructors

        #region Public Properties
        /// <summary>
        /// An identifier used to correlate this service in future requests to the broker.
        /// This MUST be globally unique within a platform marketplace. MUST be a non-empty string.
        /// Using a GUID is RECOMMENDED.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// A CLI-friendly name of the service. All lowercase, no spaces. 
        /// This MUST be globally unique within a platform marketplace.
        /// MUST be a non-empty string.
        /// </summary>
        public string Name
        {
            get
            {
                return _serviceName;
            }
            set
            {
                _serviceName = ProcessName(value);
            }
        }



        #endregion Public Properties

        #region Protected Methods
        /// <summary>
        /// Processes names based on the following rules:
        /// 1. All lowercase
        /// 2. No spaces
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string ProcessName(string value)
        {
            return value.ToLowerInvariant().Trim().RemoveSpaces();
        }

        /// <summary>
        /// Validates the object based on rules
        /// </summary>
        /// <returns>Validation error messages</returns>
        public  virtual List<ResponseBase> IsValid()
        {
            List<ResponseBase> responses = new List<ResponseBase>();

            if (string.IsNullOrEmpty(this.Name))
            {
                // MUST be a non - empty string
                responses.Add(new ResponseBase
                {
                    IsSuccess = false,
                    Description = "Name is empty"
                });
            }

             return responses;
        }
        #endregion Protected Methods
    }
}
