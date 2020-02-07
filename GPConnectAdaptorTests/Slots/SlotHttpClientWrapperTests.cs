using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Flurl.Http.Testing;
using GPConnectAdaptor;
using NSubstitute;
using Xunit.Abstractions;

namespace GPConnectAdaptorTests
{
    public class SlotHttpClientWrapperTests
    {
        private HttpTest _httpTest;
        private readonly ITestOutputHelper _output;
        private readonly string _expectedUri = "https://orange.testlab.nhs.uk/gpconnect-demonstrator/v1/fhir/" +
                                               "Slot?start=ge2020-01-21&end=le2020-01-25&status=" +
                                               "free&_include=Slot%3Aschedule&_include%3Arecurse=" +
                                               "Schedule%3Aactor%3APractitioner&searchFilter=" +
                                               "https%3A%2F%2Ffhir.nhs.uk" +
                                               "%2FSTU3%2FCodeSystem%2FGPConnect-OrganisationType-1%7Cgp-practice";

        public SlotHttpClientWrapperTests(ITestOutputHelper output)
        {
            _httpTest = new HttpTest();
            
            this._output = output;
        }

        [Fact]
        public async Task GetAsync_MakesCorrectCall()
        {
            var mockTokenGenerator = Substitute.For<IJwtTokenGenerator>();
            mockTokenGenerator.GetToken().Returns("token");
            
            _httpTest.RespondWith("abcd");
            var start = new DateTime(2020, 01, 21);
            var end = new DateTime(2020, 01, 25);
            var sut = new SlotHttpClientWrapper(mockTokenGenerator);

            var result = await sut.GetAsync(start, end);

            foreach (var call in this._httpTest.CallLog)
            {
                _output.WriteLine(call.ToString());
            }

            _httpTest.ShouldHaveCalled(_expectedUri)
                .WithHeader("Ssp-TraceID", "09a01679-2564-0fb4-5129-aecc81ea2706")
                .WithOAuthBearerToken("token")
                .Times(1);
            
            result.Should().BeEquivalentTo("abcd");
        }
    }
}