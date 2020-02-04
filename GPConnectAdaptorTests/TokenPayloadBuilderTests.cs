using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using Xunit;
using FluentAssertions;
using GPConnectAdaptor;
using GPConnectAdaptor.Models;
using Newtonsoft.Json;

namespace GPConnectAdaptorTests
{
    public class TokenPayloadBuilderTests
    {
        private readonly string _file;

        public TokenPayloadBuilderTests()
        {
            var assembly = typeof(JwtTokenGeneratorTests).GetTypeInfo().Assembly;

            using (var stream = assembly.GetManifestResourceStream("GPConnectAdaptorTests.TestJwtToken.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    _file = reader.ReadToEnd();
                }
            }
        }

        [Fact]
        public void BuildPayload_ProducesCorrectPayload()
        {
            var sut = new TokenPayloadBuilder();

            var expected = JsonConvert.DeserializeObject<JwtModel>(_file);

            var result = sut.BuildPayload();

            //Exceptions for initial and expiry times
            expected.exp = result.exp;
            expected.iat = result.iat;
            
            result.Should().BeEquivalentTo(expected);
        } 
    }
}