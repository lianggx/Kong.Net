using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kong.Common
{
    public class JsonConverterUnixDateTime : JsonConverter<DateTime>
    {
        private static DateTime Greenwich_Mean_Time = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
        private const int Limit = 10000;
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                var unixTime = reader.GetInt64();
                var dt = new DateTime(Greenwich_Mean_Time.Ticks + unixTime * Limit);
                return dt;
            }
            else
            {
                return reader.GetDateTime();
            }
        }
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var unixTime = (value - Greenwich_Mean_Time).Ticks / Limit;
            writer.WriteNumberValue(unixTime);
        }
    }
}