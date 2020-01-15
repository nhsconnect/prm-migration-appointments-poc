using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public class MigrationOrchestrator : IOrchestrator
    {
        private readonly ISlotClient _client;

        public MigrationOrchestrator(ISlotClient client)
        {
            _client = client;
        }
        public AddAppointmentResponse Orchestrate(AddAppointmentRequest request)
        {
            var slots = _client.GetSlots(request.Start, request.End);
            return new AddAppointmentResponse();
        }
    }
}