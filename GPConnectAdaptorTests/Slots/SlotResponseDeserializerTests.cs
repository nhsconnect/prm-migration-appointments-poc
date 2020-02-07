using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;
using FluentAssertions;
using GPConnectAdaptor;

namespace GPConnectAdaptorTests
{

    public class SlotResponseDeserializerTests
    {
        private readonly string[] _filePaths;
        private readonly string[] _files = new string[2];
        
        public SlotResponseDeserializerTests()
        {
            _filePaths = new[]
            {
                "GPConnectAdaptorTests.TestData.TestSlotResponse.json",
                "GPConnectAdaptorTests.TestData.TestSlotResponse2.json"
            };
            var assembly = typeof(SlotResponseDeserializerTests).GetTypeInfo().Assembly;
            
            var i = 0;
            foreach (var filePath in _filePaths)
            {
                using (var stream = assembly.GetManifestResourceStream(filePath))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        _files[i] = (reader.ReadToEnd());
                        i++;
                    }
                }
            }
        }

        [Fact]
        public void Deserialize_WhenPassedWithValidJson_DeserializesToSlotResponse()
        {
            var sut = new SlotResponseDeserializer();

            var result = sut.Deserialize(_files[0]);

            result.Should().NotBeNull();
            result.ResourceType.Should().BeEquivalentTo("Bundle");
            result.Entry.Count(e => e.Resource.ResourceType == "Slot").Should().Be(265);
        }
        
        [Fact]
        public void Deserialize_WhenPassedWithAnotherValidJson_DeserializesToSlotResponse()
        {
            var sut = new SlotResponseDeserializer();

            var result = sut.Deserialize(_files[1]);

            result.Should().NotBeNull();
            result.ResourceType.Should().BeEquivalentTo("Bundle");
        }
    }
}