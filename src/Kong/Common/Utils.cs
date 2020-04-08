using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kong.Common
{
    public class Utils
    {
        public static JsonSerializerOptions CreateJsonSetting()
        {
            var json_setting = new JsonSerializerOptions();
            json_setting.Converters.Add(new JsonStringEnumConverter());
            json_setting.Converters.Add(new JsonConverterUnixDateTime());
            json_setting.PropertyNamingPolicy = new LowercaseContractResolver();

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
