using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cresce.Api.Models;
using Cresce.Core.Appointments;
using Cresce.Core.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Cresce.Api.Controllers.Appointments
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentServices _service;

        public AppointmentsController(IAppointmentServices service) => _service = service;

        [HttpGet]
        public async Task<IEnumerable<AppointmentModel>> GetAppointments(
            [FromHeader] IEmployeeAuthorization authorization)
        {
            return (await _service.GetAppointments(authorization))
                .Select(appointment => new AppointmentModel(appointment));
        }


        [HttpPost]
        public async Task<IActionResult> CreateAppointment(
            [FromHeader] IEmployeeAuthorization authorization,
            NewAppointmentModel newAppointment)
        {
            var appointment = await _service.CreateAppointment(newAppointment.Unwrap(), authorization);
            return Created(
                $"api/v1/Appointment/{appointment.Id}",
                new AppointmentModel(appointment)
            );
        }
    }

}
