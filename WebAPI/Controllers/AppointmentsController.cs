using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace TheraFlow_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentsService _service;

        public AppointmentsController(IAppointmentsService _service)
        {
            this._service = _service;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<AppointmentDto>> GetAppointments()
        {
            return await _service.GetAllAppointmentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetAppointment(int id)
        {
            var appointment = await _service.GetAppointmentByIdAsync(id);
            return appointment == null ? NotFound() : Ok(appointment);
        }

        [HttpGet("client/{id}")]
        public async Task<IEnumerable<AppointmentDto>> GetClientsAppointments(string id)
        {
            return await _service.GetClientsAppointmentsAsync(id);
        }

        [HttpGet("specialist/{id}")]
        public async Task<IEnumerable<AppointmentDto>> GetSpecialistsAppointments(string id)
        {
            return await _service.GetSpecialistsAppointmentsAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> PostAppointment([FromForm] AppointmentDto appointment)
        {
            await _service.AddAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        [HttpPut]
        public async Task<IActionResult> PutAppointment([FromForm] AppointmentDto appointment)
        {
            await _service.UpdateAppointmentAsync(appointment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _service.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();
            await _service.DeleteAppointmentAsync(id);
            return NoContent();
        }
    }
}
