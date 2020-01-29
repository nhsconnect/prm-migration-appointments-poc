using System;
using System.IO;
using System.Net;
using System.Reflection;
using GPConnectAdaptor;
using Xunit;
using FluentAssertions;
using GPConnectAdaptor.Models;
using Newtonsoft.Json;

namespace GPConnectAdaptorTests
{
    public class JwtTokenGeneratorTests
    {
        private readonly string _file;

        public JwtTokenGeneratorTests()
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
        public void GetToken_Works()
        {
            var sut = new JwtTokenGenerator();

            var result = JsonConvert.DeserializeObject<JwtModel>(sut.GetToken());

            var expected = JsonConvert.DeserializeObject<JwtModel>(_file);

            expected.exp = result.exp;
            expected.iat = result.iat;
            
            result.Should().BeEquivalentTo(expected);
        }
    }
}