using Microsoft.AspNetCore.Mvc;
using ServiceBroker.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBroker.Core.API
{
    /// <summary>
    /// Base class for all API Controllers
    /// </summary>
    public class ApiControllerBase : Controller
    {
        #region Constants
        protected const string SERVICE_ID_KEY = "service_id";
        protected const string PLAN_ID_KEY = "plan_id";
        #endregion Constants

        #region Protected Methods
        /// <summary>
        /// Gets the query string value based on the supplied key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected object GetValueFromQS(string key)
        {
            return this.Request.Query[key].ToString();
        }

        /// <summary>
        /// Reads JSON Body request and deserializes into a typed request object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected async Task<T> GetTypedRequestFromBody<T>() where T : class
        {
            using (var reader = new StreamReader(this.Request.Body))
            {
                string body = await reader.ReadToEndAsync();

                T request = JsonHelper.Deserialize<T>(body);
                return request;
            }
        }

        #region Response Methods
        /// Generates appropriate HTTP response based on the response code set by the provider
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected IActionResult GetControllerResponse(ResponseBase response)
        {
            IActionResult result;
            switch (response.ResponseCode)
            {
                case ResponseCode.AlreadyExists:
                    result = Ok(response);
                    break;
                case ResponseCode.SuccessfulProvision:
                    result = Created("", response); // TODO : Uri as the first parameter
                    break;
                case ResponseCode.ProvisioningInProgress:
                    result = Accepted(response);
                    break;
                case ResponseCode.Conflict:
                    result = Conflict(response);
                    break;
                default:
                    result = Ok(response);
                    break;
            }
            return result;
        }
        protected ServerError ServerError(ResponseBase response)
        {
            return new ServerError(response);
        }

        protected UnprocessableEntity UnprocessableEntity(ResponseBase response)
        {
            return new API.UnprocessableEntity(response);
        }

        protected Conflict Conflict(ResponseBase response)
        {
            return new API.Conflict(response);
        }

        protected Gone Gone(ResponseBase response)
        {
            return new API.Gone(response);
        }
        #endregion Response Methods
        #endregion Protected Methods
    }
}
