using Newtonsoft.Json;


namespace ServiceBroker.Shared
{
    public class JsonHelper
    {
        public static T Deserialize<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}
