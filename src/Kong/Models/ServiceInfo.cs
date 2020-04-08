using System;
using System.Collections.Generic;

namespace Kong.Models
{
    public class ServiceInfoConllection
    {
        public List<ServiceInfo> Data { get; set; }
        public string Next { get; set; }
    }

    public class ServiceInfo
    {
        public Guid? Id { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public string Name { get; set; }
        public int Retries { get; set; }
        public string Protocol { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Path { get; set; }
        public int Connect_timeout { get; set; }
        public int Write_timeout { get; set; }
        public int Read_timeout { get; set; }
        public string[] Tags { get; set; }
    }
}
