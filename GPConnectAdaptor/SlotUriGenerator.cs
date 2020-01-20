using System;
using GPConnectAdaptor.Utilities;

namespace GPConnectAdaptor
{
    public class SlotUriGenerator : ISlotUriGenerator
    {
        private readonly string _uri = "https://orange.testlab.nhs.uk";
        private readonly string[] _includeResource = new string[]
        {
            "Schedule:actor:Practitioner"
            // "Schedule:actor:Location",
            // "Location:managingOrganization"
        };
        private readonly string[] _searchFilters = new string[]
        {
            "https://fhir.nhs.uk/STU3/CodeSystem/GPConnect-OrganisationType-1|gp-practice"
            // "https://fhir.nhs.uk/STU3/CodeSystem/GPConnect-OrganisationType-1|urgent-care",
            // "https://fhir.nhs.uk/Id/ods-organization-code|A20047"
        };
        
        public Uri GetSlotUri(DateTime start, DateTime end)
        {
            var uri = new Uri(_uri)
                    .AddQuery("start", $"ge{start.Year + "-" + start.Month + "-" + start.Day}")
                    .AddQuery("end", $"le{end.Year + end.Month + end.Day}")
                    .AddQuery("status", "free")
                    .AddQuery("_include", "Slot:schedule")
                    .AddQuery("_include:recurse", _includeResource.ToString())
                    .AddQuery("searchFilter", _searchFilters.ToString());

            var test = uri.Query.ToString();

            var test2 = uri.PathAndQuery;

            return uri;
        }
    }
}