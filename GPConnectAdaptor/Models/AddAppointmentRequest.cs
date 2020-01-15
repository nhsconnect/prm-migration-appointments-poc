using System;

namespace GPConnectAdaptor.Models
{
    public class AddAppointmentRequest
    {
        public string PatientId { get; set; }
        
        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}