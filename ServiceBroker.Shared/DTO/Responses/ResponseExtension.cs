using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceBroker.Shared
{
    public static class ResponseExtension
    {
        public static string ToMessageString(this List<ResponseBase> responses)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in responses)
            {
                builder.AppendLine(item.Description);
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}
