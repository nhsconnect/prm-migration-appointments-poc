using FluentAssertions;
using GPConnectAdaptor;
using Xunit;

namespace GPConnectAdaptorTests
{
    public class TokenEncoderTests
    {
        [Fact]
        public void Encode_WhenPassedWithCorrectPayload_ReturnsCorrectToken()
        {
            var sut = new TokenEncoder();

            var header = "eyJhbGciOiJub25lIiwidHlwIjoiSldUIn0";

            var expected = header+"."+"UEVORw=="+"."; // GPConnect Spec requires header, payload and an empty signature object concatenated with .

            var result = sut.Encode("PENG");

            result.Should().BeEquivalentTo(expected);
        }
    }
}