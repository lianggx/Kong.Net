using System;
using System.Collections.Generic;

namespace Kong.Models
{
    public class TagsInfoCollection
    {
        public List<TagsInfo> Data { get; set; }
        public Guid Offset { get; set; }
        public string Next { get; set; }
    }

    public class TagsInfo
    {
        public string Entity_name { get; set; }
        public Guid? Entity_id { get; set; }
        public string Tag { get; set; }
    }

}
