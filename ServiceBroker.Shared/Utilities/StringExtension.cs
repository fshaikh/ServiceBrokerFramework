using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBroker.Shared.Utilities
{
    public static class StringExtension
    {
        /// <summary>
        /// Removes all spaces from the given string
        /// </summary>
        /// <param name="value">Input string to operate on</param>
        /// <returns></returns>
        public static string RemoveSpaces(this string value)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                if(value[i] == ' ')
                {
                    continue;
                }
                builder.Append(value[i]);
            }
            return builder.ToString();
        }
    }
}
