using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public interface IOrchestrator
    {
        AddAppointmentResponse Orchestrate(AddAppointmentRequest request);
    }
}