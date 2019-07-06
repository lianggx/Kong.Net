using System;
using System.Collections.Generic;

namespace Kong.Models
{
    public class TargetInfoCollection
    {
        public List<TargetInfo> Data { get; set; }
        public string Next { get; set; }
    }

    public class TargetInfoAllCollection
    {
        public List<TargetInfo> Data { get; set; }
        public int total { get; set; }
    }

    public class TargetInfo
    {
        public Guid? Id { get; set; }
        public DateTime? Created_at { get; set; }
        /// <summary>
        ///  The unique identifier or the host:port attribute of the Upstream that should be associated to the newly-created Target.
        /// </summary>
        public UpStreamId UpStream { get; set; }
        public class UpStreamId
        {
            public Guid Id { get; set; }
        }
        /// <summary>
        ///  The target address (ip or hostname) and port. If the hostname resolves to an SRV record, the port value will be overridden by the value from the DNS record.
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        ///  The weight this target gets within the upstream loadbalancer (0-1000). If the hostname resolves to an SRV record, the weight value will be overridden by the value from the DNS record. Defaults to 100.
        /// </summary>
        public int Weight { get; set; }
        /// <summary>
        ///  An optional set of strings associated with the Target, for grouping and filtering.
        /// </summary>
        public string[] Tags { get; set; }
    }
}
