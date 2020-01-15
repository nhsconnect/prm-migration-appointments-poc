using System;
using GPConnectAdaptor;
using Xunit;
using FluentAssertions;

namespace GPConnectAdaptorTests
{
    public class UnitTest1
    {
        [Fact]
        public void GetToken_Works()
        {
            var sut = new JwtTokenGenerator();

            var result = sut.GetToken();

            result.Should().NotBe(null);
        }
    }
}