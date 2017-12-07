using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBroker.Shared;
using System;
using System.Threading.Tasks;

namespace ServiceBroker.Core.API
{
    /// <summary>
    /// This class implements 500 server error response
    /// </summary>
    public class ServerError :ActionResultBase, IActionResult
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="ServerError"/>
        /// </summary>
        /// <param name="response"></param>
        public ServerError(ResponseBase response):base(response)
        {
            // Do nothing
        }
        #endregion Constructors

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await base.Execute(context, StatusCodes.Status500InternalServerError);
        }
    }
}
