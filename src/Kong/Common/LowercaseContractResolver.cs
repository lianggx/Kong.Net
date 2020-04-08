using System.Text.Json;

namespace Kong.Common
{
    public class LowercaseContractResolver : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToLower();
    }
}


