using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBroker.Shared;
using System.Threading.Tasks;

namespace ServiceBroker.Core.API
{
    public class UnprocessableEntity : ActionResultBase,IActionResult
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="UnprocessableEntity"/>
        /// </summary>
        /// <param name="response"></param>
        public UnprocessableEntity(ResponseBase response):base(response)
        {
            // Do nothing
        }
        #endregion Constructors

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await base.Execute(context, StatusCodes.Status422UnprocessableEntity);
        }
    }
}
