using System;
using System.Collections.Generic;

namespace Kong.Models
{
    public class ConsumerCollection
    {
        public List<Consumer> Data { get; set; }
        public string Next { get; set; }
    }
    public class Consumer
    {
        public Guid? Id { get; set; }
        public DateTime? Created_at { get; set; }
        public string UserName { get; set; }
        public string Custom_id { get; set; }
        public string[] Tags { get; set; }
    }
}
