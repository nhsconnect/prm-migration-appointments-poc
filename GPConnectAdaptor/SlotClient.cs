using System;
using System.Net.Http;
using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public class SlotClient : ISlotClient
    {
        private readonly IJwtTokenGenerator _tokenGenerator;

        public SlotClient(IJwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
            var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0,0,0,10);
            httpClient.BaseAddress = new Uri("orange.testlab.co.uk", UriKind.Absolute);
            httpClient.DefaultRequestHeaders.Add("Ssp-TraceID", new []{"09a01679-2564-0fb4-5129-aecc81ea2706"});
        }
        public SlotResponse GetSlots(DateTime start, DateTime end)
        {
            return new SlotResponse();
        }
    }
}