using System;
using System.Collections.Generic;

namespace Kong.Models
{
    public class SNIInfoCollection
    {
        public List<SNIInfo> Data { get; set; }
        public string Next { get; set; }
    }

    public class SNIInfo
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public DateTime? Created_at { get; set; }
        public string[] Tags { get; set; }
        public CertificateId Certificate { get; set; }
        public class CertificateId
        {
            public Guid Id { get; set; }
        }
    }
}
