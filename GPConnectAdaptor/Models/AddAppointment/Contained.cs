using System.Collections.Generic;

namespace GPConnectAdaptor.Models.AddAppointment
{
    public class Contained
    {
        public string resourceType { get; set; }
        public string id { get; set; }
        public Meta meta { get; set; }
        public List<Identifier> identifier { get; set; }
        public string name { get; set; }
        public List<Telecom> telecom { get; set; }
    }
}