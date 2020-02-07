using System;
using System.Collections.Generic;

namespace GPConnectAdaptor.Models
{
    public class Meta
    {
        public string VersionId { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<string> Profile { get; set; }
    }
}