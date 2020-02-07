using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GPConnectAdaptor.Models.AddAppointment
{
    public class AddAppointmentRequest
    {
        public string resourceType { get; set; }
        public Meta meta { get; set; }
        public List<Contained> contained { get; set; }
        public string status { get; set; }
        public DateTime created { get; set; }
        public string description { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public List<Slot> slot { get; set; }
        public List<Participant> participant { get; set; }
        public List<Extension> extension { get; set; }
    }

    public class Slot
    {
        public string reference { get; set; }
    }
}