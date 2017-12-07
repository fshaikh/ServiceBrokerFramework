using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Represents Service Permissions
    /// </summary>
    public static class ServicePermissions
    {
        #region Members
        private static List<string> _permissions = new List<string>
        {
            SYSLOG_DRAIN,
            ROUTE_FORWARDING,
            VOLUME_MOUNT
        };
        #endregion Members

        #region Constants
        public const string SYSLOG_DRAIN = "syslog_drain";
        public const string ROUTE_FORWARDING = "route_forwarding";
        public const string VOLUME_MOUNT = "volume_mount";
        #endregion Constants

        #region Static Methods
        public static bool IsValid(List<string> values)
        {
            if(values == null)
            {
                return true;
            }
            for (int i = 0; i < values.Count; i++)
            {
                if(!_permissions.Contains(values[i]))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion Static Methods
    }

}
