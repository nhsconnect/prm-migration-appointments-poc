using System;
using GPConnectAdaptor.Models.AddAppointment;
using Newtonsoft.Json;

namespace GPConnectAdaptor
{
    public class AddAppointmentRequestDeserializer : IAddAppointmentRequestDeserializer
    {
        public AddAppointmentRequest Deserialize(string request)
        {
            return JsonConvert.DeserializeObject<AddAppointmentRequest>(request);
        }
    }
}