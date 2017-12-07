using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBroker.Shared
{
    public static class AssemblyExtensions
    {
        public async static Task<string> GetStringEmbeddedResource(string embeddedResourceName)
        {
            try
            {
                var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedResourceName);
                using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch(Exception exObj)
            {
                Console.WriteLine(string.Format("AssemblyExtensions::GetStringEmbeddedResource. Error in reading embedded resource: {0}. Exception :{1}", embeddedResourceName,exObj.Message));
                return string.Empty;
            }
        }
    }
}
