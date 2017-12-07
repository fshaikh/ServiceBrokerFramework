using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Contains the data necessary to activate the Dashboard SSO feature for this service.
    /// </summary>
    public class DashboardClient : DTOBase
    {
        #region Public Properties
        /// <summary>
        /// A secret for the dashboard client. If present, MUST be a non-empty string.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// A URI for the service dashboard.
        /// Validated by the OAuth token server when the dashboard requests a token.
        /// </summary>
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }
        #endregion Public Properties
    }
}
