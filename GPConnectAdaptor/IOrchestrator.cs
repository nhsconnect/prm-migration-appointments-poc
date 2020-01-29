using System.Threading.Tasks;
using GPConnectAdaptor.Models;

namespace GPConnectAdaptor
{
    public interface IOrchestrator
    {
        Task<AddAppointmentResponse> Orchestrate(AddAppointmentRequest request);
    }
}