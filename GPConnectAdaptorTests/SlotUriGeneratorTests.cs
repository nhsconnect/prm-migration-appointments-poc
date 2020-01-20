using System;
using Xunit;
using FluentAssertions;
using GPConnectAdaptor;

namespace GPConnectAdaptorTests
{
    public class SlotUriGeneratorTests
    {
        [Fact]
        public void GetSlotUri_GetsCorrectUri()
        {
            var sut = new SlotUriGenerator();

            var result = sut.GetSlotUri(DateTime.Now, DateTime.Now.AddDays(1));

            result.Should().NotBe(null);
        }
    }
}