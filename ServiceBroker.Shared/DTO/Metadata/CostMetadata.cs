namespace ServiceBroker.Shared
{
    /// <summary>
    /// This object describes the costs of a service, in what currency, and the unit of measure.
    /// </summary>
    public class CostMetadata
    {
        #region Public Properties
        /// <summary>
        /// Gets/Sets pricing in various currencies for the cost type as key-value pairs
        /// where key is currency code and value (as a float) is currency amount
        /// </summary>
        public AmountMetadata Amount { get; set; }

        /// <summary>
        /// Gets/Sets Display name for type of cost, e.g. Monthly, Hourly, Request, GB.
        /// </summary>
        public CostUnitEnum Unit { get; set; }
        #endregion Public Properties
    }
}
