using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Enumeration represents valid stae values to be sent as part of Last operation response
    /// </summary>
    public enum LastOperationStateCode
    {
        /// <summary>
        /// The platform will poll the last_operation endpoint as long as the broker returns
        /// this enum value
        /// </summary>
        
        InProgress = 1,

        /// <summary>
        /// If the provision/deprovision has succedded. This will cause the platform to cease polling
        /// </summary>
        Succeded = 2,

        /// <summary>
        /// If the provision/deprovision has failed. This will cause the platform to cease polling
        /// </summary>
        Failed = 3
    }
}
