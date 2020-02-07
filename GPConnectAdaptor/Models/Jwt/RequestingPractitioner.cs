using System.Collections.Generic;
using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public class RequestingPractitioner
    {
        public string resourceType { get; set; }
        public string id { get; set; }
        public List<Identifier> identifier { get; set; }
        public List<Name> name { get; set; }
    }
}