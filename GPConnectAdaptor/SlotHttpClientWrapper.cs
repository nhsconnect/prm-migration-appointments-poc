using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using GPConnectAdaptor.Models;
using Hl7.Fhir.Model;
using Newtonsoft.Json;

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
                .WithOAuthBearerToken(
                    "eyJhbGciOiJub25lIiwidHlwIjoiSldUIn0.ewogICJpc3MiOiAiaHR0cHM6Ly9vcmFuZ2UudGVzdGxhYi5uaHMudWsvIiwKICAic3ViIjogIjEiLAogICJhdWQiOiAiaHR0cHM6Ly9vcmFuZ2UudGVzdGxhYi5uaHMudWsvZ3Bjb25uZWN0LWRlbW9uc3RyYXRvci92MS9maGlyIiwKICAiZXhwIjogMTU4MDIwNzkwMSwKICAiaWF0IjogMTU4MDIwNzYwMSwKICAicmVhc29uX2Zvcl9yZXF1ZXN0IjogImRpcmVjdGNhcmUiLAogICJyZXF1ZXN0ZWRfc2NvcGUiOiAib3JnYW5pemF0aW9uLyoucmVhZCIsCiAgInJlcXVlc3RpbmdfZGV2aWNlIjogewogICAgInJlc291cmNlVHlwZSI6ICJEZXZpY2UiLAogICAgImlkZW50aWZpZXIiOiBbCiAgICAgIHsKICAgICAgICAic3lzdGVtIjogImh0dHBzOi8vb3JhbmdlLnRlc3RsYWIubmhzLnVrL2dwY29ubmVjdC1kZW1vbnN0cmF0b3IvSWQvbG9jYWwtc3lzdGVtLWluc3RhbmNlLWlkIiwKICAgICAgICAidmFsdWUiOiAiZ3BjZGVtb25zdHJhdG9yLTEtb3JhbmdlIgogICAgICB9CiAgICBdLAogICAgIm1vZGVsIjogIkdQIENvbm5lY3QgRGVtb25zdHJhdG9yIiwKICAgICJ2ZXJzaW9uIjogIjEuMi4zIgogIH0sCiAgInJlcXVlc3Rpbmdfb3JnYW5pemF0aW9uIjogewogICAgInJlc291cmNlVHlwZSI6ICJPcmdhbml6YXRpb24iLAogICAgImlkZW50aWZpZXIiOiBbCiAgICAgIHsKICAgICAgICAic3lzdGVtIjogImh0dHBzOi8vZmhpci5uaHMudWsvSWQvb2RzLW9yZ2FuaXphdGlvbi1jb2RlIiwKICAgICAgICAidmFsdWUiOiAiQTExMTExIgogICAgICB9CiAgICBdLAogICAgIm5hbWUiOiAiQ29uc3VtZXIgb3JnYW5pc2F0aW9uIG5hbWUiCiAgfSwKICAicmVxdWVzdGluZ19wcmFjdGl0aW9uZXIiOiB7CiAgICAicmVzb3VyY2VUeXBlIjogIlByYWN0aXRpb25lciIsCiAgICAiaWQiOiAiMSIsCiAgICAiaWRlbnRpZmllciI6IFsKICAgICAgewogICAgICAgICJzeXN0ZW0iOiAiaHR0cHM6Ly9maGlyLm5ocy51ay9JZC9zZHMtdXNlci1pZCIsCiAgICAgICAgInZhbHVlIjogIjExMTExMTExMTExMSIKICAgICAgfSwKICAgICAgewogICAgICAgICJzeXN0ZW0iOiAiaHR0cHM6Ly9maGlyLm5ocy51ay9JZC9zZHMtcm9sZS1wcm9maWxlLWlkIiwKICAgICAgICAidmFsdWUiOiAiMjIyMjIyMjIyMjIyMjIiCiAgICAgIH0sCiAgICAgIHsKICAgICAgICAic3lzdGVtIjogImh0dHBzOi8vb3JhbmdlLnRlc3RsYWIubmhzLnVrL2dwY29ubmVjdC1kZW1vbnN0cmF0b3IvSWQvbG9jYWwtdXNlci1pZCIsCiAgICAgICAgInZhbHVlIjogIjEiCiAgICAgIH0KICAgIF0sCiAgICAibmFtZSI6IFsKICAgICAgewogICAgICAgICJmYW1pbHkiOiAiRGVtb25zdHJhdG9yIiwKICAgICAgICAiZ2l2ZW4iOiBbCiAgICAgICAgICAiR1BDb25uZWN0IgogICAgICAgIF0sCiAgICAgICAgInByZWZpeCI6IFsKICAgICAgICAgICJEciIKICAgICAgICBdCiAgICAgIH0KICAgIF0KICB9Cn0."
                    )
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
    
    
    
    public class UntrustedCertClientFactory : DefaultHttpClientFactory
    {
        public override HttpMessageHandler CreateMessageHandler() {
            return new HttpClientHandler {
                ServerCertificateCustomValidationCallback = (a, b, c, d) => true
            };
        }
    }
}