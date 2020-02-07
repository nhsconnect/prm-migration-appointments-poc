using System;
using System.Linq;
using System.Threading.Tasks;
using GPConnectAdaptor.Models.AddAppointment;

namespace GPConnectAdaptor
{
    public class MigrationOrchestrator : IOrchestrator
    {
        private readonly ISlotClient _slotClient;
        private readonly IAddAppointmentClient _addAppointmentClient;

        public MigrationOrchestrator(ISlotClient slotClient, IAddAppointmentClient addAppointmentClient)
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
            var slot = slotResponse.Entry
                .Select(e => e.Resource)
                .Where(r => r.ResourceType == "Slot")
                .First(s => s.Start >= request.Start && s.End <= request.End);

            var scheduleId = slot.Schedule.Reference.Substring(9);

            var locationId = slotResponse.Entry.Select(e => e.Resource)
                .Where(r => r.ResourceType == "Schedule")
                .First(s => s.Id == scheduleId)
                .Actor.First(a => a.Reference.StartsWith("Location/")).Reference;


            return new AddAppointmentCriteria()
            {
                PatientId = request.PatientId,
                LocationId = locationId,
                SlotReference = slot.Id,
                Start = slot.Start ?? new DateTime(),
                End = slot.End ?? new DateTime()
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