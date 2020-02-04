using System;
using System.Collections.Generic;
using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public class TokenPayloadBuilder : ITokenPayloadBuilder
    {
        public JwtModel BuildPayload()
        {
            return new JwtModel()
            {
                iss = "https://orange.testlab.nhs.uk/",
                sub = "1",
                aud = "https://orange.testlab.nhs.uk/gpconnect-demonstrator/v1/fhir",
                exp = (int)(DateTime.UtcNow.AddMinutes(5) - new DateTime(1970, 1, 1)).TotalSeconds,
                iat = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds,
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
                    name = "Consumer organisation name"
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
        }
    }
}