using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace GPConnectAdaptor
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GetToken()
        {
            
			// var claims = new List<System.Security.Claims.Claim>
			// 	{
			// 	    new System.Security.Claims.Claim("iss", requestingSystemUrl, ClaimValueTypes.String),
			// 	    new System.Security.Claims.Claim("sub", requestingPractitioner.Id, ClaimValueTypes.String),
			// 	    new System.Security.Claims.Claim("aud", requestingSystemTokenUrl, ClaimValueTypes.String),
			// 	    new System.Security.Claims.Claim("exp", EpochTime.GetIntDate(expires).ToString(), ClaimValueTypes.Integer64),
			// 	    new System.Security.Claims.Claim("iat", EpochTime.GetIntDate(now).ToString(), ClaimValueTypes.Integer64),
			// 	    new System.Security.Claims.Claim("reason_for_request", "directcare", ClaimValueTypes.String),
			// 	    new System.Security.Claims.Claim("requested_scope", "organization/*.read", ClaimValueTypes.String),
			// 	    new System.Security.Claims.Claim("requesting_device", JsonConvert.SerializeObject(requestingDevice), JsonClaimValueTypes.Json),
			// 	    new System.Security.Claims.Claim("requesting_organization", JsonConvert.SerializeObject(requestingOrganization), JsonClaimValueTypes.Json),
			// 	    new System.Security.Claims.Claim("requesting_practitioner", JsonConvert.SerializeObject(requestingPractitioner), JsonClaimValueTypes.Json)
			// 	};

	        // Serialize To Json
	        
	        var payload = new JwtModel()
	        {
		        iss = "https://orange.testlab.nhs.uk/",
		        sub = "1",
		        aud = "https://orange.testlab.nhs.uk/gpconnect-demonstrator/v1/fhir",
		        exp = 1579022678,
		        iat = 1579022378,
		        reason_for_request = "directcare",
		        requested_scope = "organization/*.read",
		        requesting_device = new RequestingDevice()
		        {
			        resourceType = "Device",
			        identifier = new List<Identifier>()
			        {
				        new Identifier()
				        {
					        system = "https://orange.testlab.nhs.uk/gpconnect-demonstrator/Id/local-system-instance-id",
					        value = "gpcdemonstrator-1-orange"
				        }
			        },
			        model = "GP Connect Demonstrator",
			        version = "1.2.3"
		        },
		        requesting_organization = new RequestingOrganization()
		        {
			        resourceType = "Organization",
			        identifier = new List<Identifier>()
			        {
				        new Identifier()
				        {
					        system = "https://fhir.nhs.uk/Id/ods-organization-code",
					        value = "A11111"
				        }
			        },
			        name = "Consumer Organisation Name"
		        },
		        requesting_practitioner = new RequestingPractitioner()
		        {
			        resourceType = "Practitioner",
			        id = "1",
			        identifier = new List<Identifier>()
			        {
				        new Identifier()
				        {
					        system = "https://fhir.nhs.uk/Id/sds-user-id",
					        value = "111111111111"
				        },
				        new Identifier()
				        {
					        system = "https://fhir.nhs.uk/Id/sds-role-profile-id",
					        value = "22222222222222"
				        },
				        new Identifier()
				        {
					        system = "https://orange.testlab.nhs.uk/gpconnect-demonstrator/Id/local-user-id",
					        value = "1"
				        }
			        },
			        name = new List<Name>()
			        {
				        new Name()
				        {
					        family = "Demonstrator",
					        given = new List<string>()
					        {
						        "GPConnect"
					        },
					        prefix = new List<string>()
					        {
						        "Dr"
					        }
				        }
			        }
		        }

	        };

	        return JsonConvert.SerializeObject(payload);
        }
    }
    
    public class Identifier
    {
	    public string system { get; set; }
	    public string value { get; set; }
    }

    public class RequestingDevice
    {
	    public string resourceType { get; set; }
	    public List<Identifier> identifier { get; set; }
	    public string model { get; set; }
	    public string version { get; set; }
    }

    public class RequestingOrganization
    {
	    public string resourceType { get; set; }
	    public List<Identifier> identifier { get; set; }
	    public string name { get; set; }
    }

    public class Name
    {
	    public string family { get; set; }
	    public List<string> given { get; set; }
	    public List<string> prefix { get; set; }
    }

    public class RequestingPractitioner
    {
	    public string resourceType { get; set; }
	    public string id { get; set; }
	    public List<Identifier> identifier { get; set; }
	    public List<Name> name { get; set; }
    }

    public class JwtModel
    {
	    public string iss { get; set; }
	    public string sub { get; set; }
	    public string aud { get; set; }
	    public int exp { get; set; }
	    public int iat { get; set; }
	    public string reason_for_request { get; set; }
	    public string requested_scope { get; set; }
	    public RequestingDevice requesting_device { get; set; }
	    public RequestingOrganization requesting_organization { get; set; }
	    public RequestingPractitioner requesting_practitioner { get; set; }
    }
}