using System.Collections.Generic;
using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public class RequestingOrganization
    {
        public string resourceType { get; set; }
        public List<Identifier> identifier { get; set; }
        public string name { get; set; }
    }
}