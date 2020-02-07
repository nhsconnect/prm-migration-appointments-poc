using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace GPConnectAdaptor
{
    public class SlotHttpClientWrapper : ISlotHttpClientWrapper
    {
        private readonly string _uri = "https://orange.testlab.nhs.uk/";
        private readonly string _searchFilter =
            "https://fhir.nhs.uk/STU3/CodeSystem/GPConnect-OrganisationType-1|gp-practice";
        private readonly string _traceId = "09a01679-2564-0fb4-5129-aecc81ea2706";
        private readonly string _consumerAsid = "200000000359";
        private readonly string _providerAsid = "918999198993";
        private readonly string _sdsInteractionId = "urn:nhs:names:services:gpconnect:fhir:rest:search:slot-1";
        private readonly IJwtTokenGenerator _tokenGenerator;

        public SlotHttpClientWrapper(IJwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
            FlurlHttp.ConfigureClient(_uri, cli =>
                cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
        }
        
        public async Task<string> GetAsync(DateTime start, DateTime end)
        {
            var temp = _uri
                .AppendPathSegment("gpconnect-demonstrator/v1/fhir/Slot")
                .WithHeaders(new
                {
                    Ssp_TraceID = _traceId,
                    Ssp_From = _consumerAsid,
                    Ssp_To = _providerAsid,
                    Ssp_InteractionID = _sdsInteractionId,
                    Accept = "application/fhir+json"
                })
                .WithOAuthBearerToken(_tokenGenerator.GetToken())
                .SetQueryParams(new
                {
                    start = $"ge{start:yyyy-MM-dd}",
                    end = $"le{end:yyyy-MM-dd}",
                    status = "free",
                    _include = "Slot:schedule"
                })
                .SetQueryParam(Url.Encode("_include:recurse"), "Schedule:actor:Practitioner", false)
                .SetQueryParam("searchFilter", _searchFilter, false);

            return await temp.GetStringAsync();
        }
    }
}