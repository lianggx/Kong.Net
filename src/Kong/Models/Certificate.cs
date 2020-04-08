using System;
using System.Collections.Generic;

namespace Kong.Models
{
    public class CertificateCollection
    {
        public List<Certificate> Data { get; set; }
        public string Next { get; set; }
    }

    public class Certificate
    {
        public Guid? Id { get; set; }
        public DateTime? Created_at { get; set; }
        public string Cert { get; set; }
        public string Key { get; set; }
        public string[] Tags { get; set; }
    }
}
