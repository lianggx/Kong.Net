using System;
using System.Collections.Generic;

namespace Kong.Models
{
    public class PluginCollection
    {
        public List<Plugin> Data { get; set; }
        public string Next { get; set; }
    }

    public class Plugin
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public DateTime? Created_at { get; set; }
        public RouteId Route { get; set; }
        public class RouteId
        {
            public Guid Id { get; set; }
        }
        public ServiceId Service { get; set; }

        public class ServiceId
        {
            public Guid Id { get; set; }
        }
        public ConsumerId Consumer { get; set; }

        public class ConsumerId
        {
            public Guid Id { get; set; }
        }
        public PluginConfig Config { get; set; }
        public string Run_on { get; set; }
        public string[] Protocols { get; set; }
        public bool Enabled { get; set; }
        public string[] Tags { get; set; }
    }

    public class PluginConfig
    {
        public int Minute { get; set; }
        public int Hour { get; set; }
    }

    public class PluginEnabled
    {
        public string[] Enabled_plugins { get; set; }
    }

    public class PluginSchema
    {
        public System.Text.Json.JsonDocument Fields { get; set; }
    }
}
