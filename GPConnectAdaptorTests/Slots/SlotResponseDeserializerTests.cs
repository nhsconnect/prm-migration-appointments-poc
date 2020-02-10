using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using GPConnectAdaptor.Slots;
using Xunit;

namespace GPConnectAdaptorTests.Slots
{

    public class SlotResponseDeserializerTests
    {
        private readonly string[] _filePaths;
        private readonly string[] _files = new string[3];
        
        public SlotResponseDeserializerTests()
        {
            _filePaths = new[]
            {
                "GPConnectAdaptorTests.TestData.TestSlotResponse.json",
                "GPConnectAdaptorTests.TestData.TestSlotResponse2.json",
                "GPConnectAdaptorTests.TestData.TestSlotResponse3.json"
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
            result.resourceType.Should().BeEquivalentTo("Bundle");
            result.entry.Count(e => e.resource.resourceType == "Slot").Should().Be(265);
        }
        
        [Fact]
        public void Deserialize_WhenPassedWithAnotherValidJson_DeserializesToSlotResponse()
        {
            var sut = new SlotResponseDeserializer();

            var result = sut.Deserialize(_files[1]);

            result.Should().NotBeNull();
            result.resourceType.Should().BeEquivalentTo("Bundle");
        }
        
        [Fact]
        public void Deserialize_WhenPassedWithUsePropertyUnderNAme_DeserializesToSlotResponse()
        {
            var sut = new SlotResponseDeserializer();

            var result = sut.Deserialize(_files[2]);

            result.Should().NotBeNull();
            result.resourceType.Should().BeEquivalentTo("Bundle");
        }
    }
}