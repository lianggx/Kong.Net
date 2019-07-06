using Newtonsoft.Json;
using System;

namespace Kong.Common
{
    public class Utils
    {
        public static JsonSerializerSettings CreateJsonSetting()
        {
            var json_setting = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            json_setting.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            json_setting.Converters.Add(new DateTimeConverter());
            json_setting.ContractResolver = new LowercaseContractResolver();

            return json_setting;
        }

        public static string CreateNextUri(string sourceUri, string next)
        {
            string path = sourceUri;
            if (next != null)
            {
                var uri = new Uri(next);
                path = string.Format("{0}{1}", sourceUri, uri.Query);
            }
            return path;
        }
    }
}
