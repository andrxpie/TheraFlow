using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace TheraFlow_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _sccheduleService;

        public ScheduleController(IScheduleService consultationNotesService)
        {
            _sccheduleService = consultationNotesService;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<ScheduleDto>> GetSchedules()
        {
            return await _sccheduleService.GetAllScheduleAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleDto>> GetSchedule(int id)
        {
            var schedule = await _sccheduleService.GetScheduleByIdAsync(id);
            return schedule == null ? NotFound() : Ok(schedule);
        }

        [HttpGet("specialist/{id}")]
        public async Task<IEnumerable<ScheduleDto>> GetSchedulesBySpecialist(int id)
        {
            return await _sccheduleService.GetSchedulesBySpecialistAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostSchedule([FromForm] ScheduleDto schedule)
        {
            try
            {
                await _sccheduleService.AddScheduleAsync(schedule); ;
                return CreatedAtAction(nameof(schedule), new { id = schedule }, schedule);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding schedule", ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutSchedule([FromForm] ScheduleDto schedule)
        {
            try
            {
                await _sccheduleService.UpdateScheduleAsync(schedule);
                return CreatedAtAction(nameof(GetSchedule), new { id = schedule }, schedule);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating schedule", ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _sccheduleService.GetScheduleByIdAsync(id);
            if (schedule == null) return NotFound();
            await _sccheduleService.DeleteScheduleAsync(id);
            return NoContent();
        }
    }
}
