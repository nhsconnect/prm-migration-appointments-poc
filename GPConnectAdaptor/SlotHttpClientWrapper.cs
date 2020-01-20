using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GPConnectAdaptor.Models;
using Hl7.Fhir.Model;
using Newtonsoft.Json;

namespace GPConnectAdaptor
{
    public class SlotHttpClientWrapper : ISlotHttpClientWrapper
    {
        private readonly string _uri = "https://orange.testlab.nhs.uk";
        private readonly string _traceId = "09a01679-2564-0fb4-5129-aecc81ea2706";
        private readonly string _consumerAsid = "200000000359";
        private readonly string _providerAsid = "918999198993";
        private readonly string _sdsInteractionId = "urn:nhs:names:services:gpconnect:fhir:rest:search:slot-1";
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly ISlotUriGenerator _slotUriGenerator;

        public HttpClient Client { get; set; }
        
        public SlotHttpClientWrapper(IJwtTokenGenerator tokenGenerator, ISlotUriGenerator slotUriGenerator)
        {
            _tokenGenerator = tokenGenerator;
            _slotUriGenerator = slotUriGenerator;
            Client = GenerateClient();
        }

        public async Task<string> GetAsync(DateTime start, DateTime end)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Authorization", 
                _tokenGenerator.GetToken()
            );

            var uri = _slotUriGenerator.GetSlotUri(start, end);

            return await Client.GetStringAsync(uri);
        }

        private HttpClient GenerateClient()
        {
            var client = new HttpClient();
            client.Timeout = new TimeSpan(0,0,0,10);
            client.BaseAddress = new Uri(_uri, UriKind.Absolute);
            client.DefaultRequestHeaders.Add("Ssp-TraceID", new []{_traceId});
            client.DefaultRequestHeaders.Add("Ssp-From", new []{_consumerAsid});
            client.DefaultRequestHeaders.Add("Ssp-To", new []{_providerAsid});
            client.DefaultRequestHeaders.Add("Ssp-InteractionID", new []{_sdsInteractionId});

            return client;
        }
        
    }
}