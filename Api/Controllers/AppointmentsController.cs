using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GPConnectAdaptor;
using GPConnectAdaptor.Models;
using GPConnectAdaptor.Models.AddAppointment;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {

    private readonly ILogger<AppointmentsController> _logger;
    private readonly IOrchestrator _orchestrator;

        public AppointmentsController(ILogger<AppointmentsController> logger, IOrchestrator orchestrator)
        {
            _logger = logger;
            _orchestrator = orchestrator;
        }

        [HttpPost]
        public async Task<AddAppointmentResponse> AddAppointment([FromBody] TempAddAppointmentRequest request)
        {
            var slotInfo = _orchestrator.GetSlotInfo(request);
            var appointment = _orchestrator.AddAppointment(await slotInfo);
            return await appointment;
        }
    }
}
