using System;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.AddAppointment;
using Newtonsoft.Json;
using AppointmentResponse = Hl7.Fhir.Model.AppointmentResponse;

namespace GPConnectAdaptor
{
    public class AddAppointmentClient : IAddAppointmentClient
    {
        private readonly IAddAppointmentRequestBuilder _addAppointmentRequestBuilder;
        private readonly IAddAppointmentHttpClientWrapper _httpClientWrapper;
        private IAddAppointmentResponseDeserializer _addAppointmentResponseDeserializer;

        public AddAppointmentClient(IJwtTokenGenerator tokenGenerator,
            IAddAppointmentRequestBuilder addAppointmentRequestBuilder,
            IAddAppointmentHttpClientWrapper httpClientWrapper,
            IAddAppointmentResponseDeserializer addAppointmentResponseDeserializer)
        {
            _addAppointmentRequestBuilder = addAppointmentRequestBuilder;
            _httpClientWrapper = httpClientWrapper;
            _addAppointmentResponseDeserializer = addAppointmentResponseDeserializer;
        }
        
        public async Task<AddAppointmentResponse> AddAppointment(string slotRef,
            string patientRef,
            string locationRef,
            DateTime start,
            DateTime end)
        {
            var request = _addAppointmentRequestBuilder.Build(slotRef, patientRef, locationRef, start, end);
            var appointmentRequestBody = JsonConvert.SerializeObject(request);
            var appointmentResponseBody = await _httpClientWrapper.PostAsync(appointmentRequestBody);
            var appointment = _addAppointmentResponseDeserializer.Deserialize(appointmentResponseBody);

            return appointment;
        }
    }
}