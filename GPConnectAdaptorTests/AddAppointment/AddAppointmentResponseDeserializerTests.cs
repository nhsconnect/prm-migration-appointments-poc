using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using GPConnectAdaptor;
using Xunit;

namespace GPConnectAdaptorTests
{
    public class AddAppointmentResponseDeserializerTests
    {
        private readonly string _appointmentSuccessPath =
            "GPConnectAdaptorTests.TestData.AddAppointment.AppointmentResponse.json";
        private readonly string _appointmentFailPath =
            "GPConnectAdaptorTests.TestData.AddAppointment.FailedAppointmentResponse.json";
        private Dictionary<string, string> _filePaths;
        private readonly Dictionary<string, string> _files = new Dictionary<string, string>();

        public AddAppointmentResponseDeserializerTests()
        {
            PopulatePaths();
            PopulateResponses();
        }
        
        [Fact]
        public void Deserialize_WhenAppointmentBookingSuccess_ParsesIntoAppointmentResponse()
        {
            var response = _files["success"];
            
            var sut = new AddAppointmentResponseDeserializer();

            var result = sut.Deserialize(response);

            result.Should().NotBeNull();

        }
        
        [Fact]
        public void Deserialize_WhenAppointmentBookingFail_ParsesIntoAppointmentResponse()
        {
            var response = _files["fail"];
            
            var sut = new AddAppointmentResponseDeserializer();

            var result = sut.Deserialize(response);

            result.Should().NotBeNull();

        }
        
        private void PopulatePaths()
        {
            _filePaths = new Dictionary<string, string>()
            {
                {
                    "success", _appointmentSuccessPath
                },
                {
                    "fail", _appointmentFailPath
                }
            };
        }

        private void PopulateResponses()
        {
            var assembly = typeof(AddAppointmentResponseDeserializerTests).GetTypeInfo().Assembly;

            foreach (var filePath in _filePaths)
            {
                using (var stream = assembly.GetManifestResourceStream(filePath.Value))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        _files.Add(filePath.Key, reader.ReadToEnd());
                    }
                }
            }
        }
    }
}