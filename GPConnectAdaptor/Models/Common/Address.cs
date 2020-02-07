using System.Collections.Generic;

namespace GPConnectAdaptor.Models
{
    public class Address
    {
        public string Use { get; set; }
        public List<string> Line { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
    }
}