using System;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.AddAppointment;

namespace GPConnectAdaptor
{
    public class MigrationOrchestrator : IOrchestrator
    {
        private readonly Slots.ISlotClient _slotClient;
        private readonly IAddAppointmentClient _addAppointmentClient;

        public MigrationOrchestrator(Slots.ISlotClient slotClient, IAddAppointmentClient addAppointmentClient)
        {
            _slotClient = slotClient;
            _addAppointmentClient = addAppointmentClient;
        }
        
        /// <summary>
        /// This will replace all public methods here
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AddAppointmentResponse> Orchestrate(TempAddAppointmentRequest request)
        {
            var slotResponse =  await _slotClient.GetSlots(request.Start, request.End);
            
            
            return new AddAppointmentResponse();
        }

        public async Task<AddAppointmentResponse> AddAppointment(AddAppointmentCriteria criteria)
        {
            return await _addAppointmentClient.AddAppointment(
                criteria.SlotReference,
                criteria.PatientId,
                criteria.LocationId,
                criteria.Start, criteria.End);
        }
        
        
        /// <summary>
        /// This is temporary and obsolete. Will be removed.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AddAppointmentCriteria> GetSlotInfo(TempAddAppointmentRequest request)
        {
            var slotResponse =  await _slotClient.GetSlots(request.Start, request.End);
            var slot = slotResponse.entry
                .Select(e => e.resource)
                .Where(r => r.resourceType == "Slot")
                .First(s => 
                    s.start >= request.Start.Subtract(new TimeSpan(0,0,1)) && 
                    s.end <= request.End.AddSeconds(1));

            var scheduleId = slot.schedule.reference.Substring(9);

            var locationId = slotResponse.entry.Select(e => e.resource)
                .Where(r => r.resourceType == "Schedule")
                .First(s => s.id == scheduleId)
                .actor.First(a => a.reference.StartsWith("Location/")).reference;


            return new AddAppointmentCriteria()
            {
                PatientId = request.PatientId,
                LocationId = locationId,
                SlotReference = slot.id,
                Start = slot.start ?? new DateTime(),
                End = slot.end ?? new DateTime()
            };
        }
    }
    
    /// <summary>
    /// Temporary, will be obsolete
    /// </summary>
    public class AddAppointmentCriteria
    {
        public string SlotReference { get; set; }
        public string PatientId { get; set; }
        public string LocationId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
    
    /// <summary>
    /// Temporary and will be obsolete
    /// </summary>
    public class TempAddAppointmentRequest
    {
        public string PatientId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}