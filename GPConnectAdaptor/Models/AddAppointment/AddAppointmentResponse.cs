using System;
using System.Collections.Generic;

namespace GPConnectAdaptor.Models.AddAppointment
{
    public class AddAppointmentResponse
    {
        public string resourceType { get; set; }
        public string id { get; set; }
        public Meta meta { get; set; }
        public List<Contained> contained { get; set; }
        public List<Extension> extension { get; set; }
        public string status { get; set; }
        public int priority { get; set; }
        public string description { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int minutesDuration { get; set; }
        public List<Hl7.Fhir.Model.Slot> slot { get; set; }
        public DateTime created { get; set; }
        public List<Participant> participant { get; set; }
        public List<Issue> issue { get; set; }
    }
    
    public class Details
    {
        public List<Coding> coding { get; set; }
    }

    public class Issue
    {
        public string severity { get; set; }
        public string code { get; set; }
        public Details details { get; set; }
        public string diagnostics { get; set; }
    }
}