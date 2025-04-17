using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace TheraFlow_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultationNotesController : ControllerBase
    {
        private readonly IConsultationNotesService _consultationNotesService;

        public ConsultationNotesController(IConsultationNotesService consultationNotesService)
        {
            _consultationNotesService = consultationNotesService;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<ConsultationNoteDto>> GetConsultationNotes()
        {
            return await _consultationNotesService.GetAllConsultationNotesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultationNoteDto>> GetConsultationNote(int id)
        {
            var consultationNote = await _consultationNotesService.GetConsultationNoteByIdAsync(id);
            return consultationNote == null ? NotFound() : Ok(consultationNote);
        }

        [HttpGet("appointment/{id}")]
        public async Task<IEnumerable<ConsultationNoteDto>> GetConsultationNotesByAppointment(int id)
        {
            return await _consultationNotesService.GetConsultationNotesByAppointmentAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostConsultationNote([FromForm] ConsultationNoteDto consultationNote)
        {
            try
            {
                await _consultationNotesService.AddConsultationNoteAsync(consultationNote); ;
                return CreatedAtAction(nameof(consultationNote), new { id = consultationNote }, consultationNote);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding consultation note", ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutConsultationNote([FromForm] ConsultationNoteDto consultationNote)
        {
            try
            {
                await _consultationNotesService.UpdateConsultationNoteAsync(consultationNote);
                return CreatedAtAction(nameof(GetConsultationNote), new { id = consultationNote }, consultationNote);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating consultation note", ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsultationNote(int id)
        {
            var consultationNote = await _consultationNotesService.GetConsultationNoteByIdAsync(id);
            if (consultationNote == null) return NotFound();
            await _consultationNotesService.DeleteConsultationNoteAsync(id);
            return NoContent();
        }
    }
}
