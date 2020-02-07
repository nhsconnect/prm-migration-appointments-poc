using GPConnectAdaptor.Models.AddAppointment;
using Newtonsoft.Json;

namespace GPConnectAdaptor
{
    public class AddAppointmentResponseDeserializer : IAddAppointmentResponseDeserializer
    {
        public AddAppointmentResponse Deserialize(string response)
        {
            return JsonConvert.DeserializeObject<AddAppointmentResponse>(response);
        }
    }
}