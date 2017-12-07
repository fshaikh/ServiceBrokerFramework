using Microsoft.AspNetCore.Mvc;
using ServiceBroker.Shared;
using System.Threading.Tasks;

namespace ServiceBroker.Core.API
{
    /// <summary>
    /// Base class for all custom action results
    /// </summary>
    public abstract class ActionResultBase
    {
        #region Members
        private readonly ResponseBase _response;
        #endregion Members

        #region Constructors
        /// <summary>
        ///
        /// </summary>
        /// <param name="response"></param>
        public ActionResultBase(ResponseBase response)
        {
            _response = response;
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets the response 
        /// </summary>
        public ResponseBase Response
        {
            get
            {
                return _response;
            }
        }
        #endregion Properties

        #region Methods
        protected async Task Execute(ActionContext context, int? statusCode)
        {
            var jsonResult = new JsonResult(_response)
            {
                StatusCode = statusCode
            };
            await jsonResult.ExecuteResultAsync(context);
        }
        #endregion Methods
    }
}
