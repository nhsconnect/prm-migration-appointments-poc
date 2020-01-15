using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GPConnectAdaptor;
using GPConnectAdaptor.Models;

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
        public IActionResult AddAppointment([FromBody] AddAppointmentRequest request)
        {
            var response = _orchestrator.Orchestrate(request);
            return new JsonResult(response);
        }
    }
}
