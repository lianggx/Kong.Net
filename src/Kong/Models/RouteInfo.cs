using System;
using System.Collections.Generic;

namespace Kong.Models
{
    public class RouteInfoCollection
    {
        public List<RouteInfo> Data { get; set; }
        public string Next { get; set; }
    }

    public class RouteInfo
    {
        public Guid? Id { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public string Name { get; set; }
        public string[] Protocols { get; set; }
        public string[] Methods { get; set; }
        public string[] Hosts { get; set; }
        public string[] Paths { get; set; }
        public int Https_redirect_status_code { get; set; }
        public int Regex_priority { get; set; }
        public bool Strip_path { get; set; }
        public bool Preserve_host { get; set; }
        public string[] Tags { get; set; }
        public ServiceId Service { get; set; }

        public class ServiceId
        {
            public Guid Id { get; set; }
        }
    }
}
