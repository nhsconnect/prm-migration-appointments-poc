using System.Collections.Generic;
using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public class RequestingDevice
    {
        public string resourceType { get; set; }
        public List<Identifier> identifier { get; set; }
        public string model { get; set; }
        public string version { get; set; }
    }
}