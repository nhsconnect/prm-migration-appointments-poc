namespace GPConnectAdaptor.Models
{
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