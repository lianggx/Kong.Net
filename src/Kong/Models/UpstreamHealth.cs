using System;
using System.Collections.Generic;

namespace Kong.Models
{
    public class UpstreamHealthCollection
    {
        public int Total { get; set; }
        public Guid? Node_id { get; set; }
        public List<UpstreamHealth> Data { get; set; }
    }

    public class UpstreamHealth
    {
        public DateTime? Created_at { get; set; }
        public Guid? Id { get; set; }
        public string Health { get; set; }
        public string Target { get; set; }
        public Guid UpStream_id { get; set; }
        public int Weight { get; set; }
    }
}
