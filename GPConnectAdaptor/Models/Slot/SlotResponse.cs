using System.Collections.Generic;

namespace GPConnectAdaptor.Models
{ 
    public class SlotResponse
    {
        public string ResourceType { get; set; }
        public string Id { get; set; }
        public Meta Meta { get; set; }
        public string Type { get; set; }
        public List<Link> Link { get; set; }
        public List<Entry> Entry { get; set; }
    }
}