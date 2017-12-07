using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBroker.Shared
{
    public class AmountMetadata
    {
        #region Public Properties
        /// <summary>
        /// Gets/Sets the Currency Code
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets/Sets the Value (currency amount)
        /// </summary>
        public float Value { get; set; }
        #endregion Public Properties
    }
}
