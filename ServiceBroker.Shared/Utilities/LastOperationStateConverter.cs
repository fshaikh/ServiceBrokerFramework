using Newtonsoft.Json;
using System;

namespace ServiceBroker.Shared
{
    /// <summary>
    /// Custom JSON Converter for LastOperationState Code enum. Based on the SPEC:
    /// Valid values for state are " in progress, succeeded, and failed
    /// </summary>
    public class LastOperationStateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LastOperationStateCode);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            string val = GetStateStringValue(value);
            writer.WriteValue(val);
        }

        private string GetStateStringValue(object value)
        {
            LastOperationStateCode code = (LastOperationStateCode)value;
            string returnVal = string.Empty;
            switch (code)
            {
                case LastOperationStateCode.Succeded:
                    returnVal = "succeeded";
                    break;
                case LastOperationStateCode.InProgress:
                    returnVal = "in progress";
                    break;
                case LastOperationStateCode.Failed:
                    returnVal = "failed";
                    break;
            }
            return returnVal;
        }
    }
}
