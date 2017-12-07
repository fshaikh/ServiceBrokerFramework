using System.Collections.Generic;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Represents Plan Metadata
    /// </summary>
    public class PlanMetadata
    {
        #region Public Properties
        /// <summary>
        /// Gets/Sets Features of this plan, to be displayed in a bulleted-list
        /// </summary>
        public List<string> Bullets { get; set; }

        /// <summary>
        /// An array-of-objects that describes the costs of a service, in what currency, and the unit of measure.
        /// If there are multiple costs, all of them could be billed to the user (such as a monthly + usage costs at once).
        /// </summary>
        public List<CostMetadata> Costs { get; set; }

        /// <summary>
        /// Gets/Sets Name of the plan to be displayed to clients
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets/Sets any custom metadata for a plan
        /// </summary>
        public object CustomMeta { get; set; }
        #endregion Public Properties
    }
}
