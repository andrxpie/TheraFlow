using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace TheraFlow_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialistController : ControllerBase
    {
        private readonly ISpecialistService _service;

        public SpecialistController(ISpecialistService _service)
        {
            this._service = _service;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<SpecialistDto>> GetSpecialists()
        {
            return await _service.GetAllSpecialistsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpecialistDto>> GetSpecialist(int id)
        {
            var specialist = await _service.GetSpecialistByIdAsync(id);
            return specialist == null ? NotFound() : Ok(specialist);
        }

        [HttpPost]
        public async Task<ActionResult<SpecialistDto>> PostSpecialist([FromForm] SpecialistDto specialist)
        {
            await _service.AddSpecialistAsync(specialist);
            return CreatedAtAction(nameof(GetSpecialist), new { id = specialist.Id }, specialist);
        }

        [HttpPut]
        public async Task<IActionResult> PutSpecialist([FromForm] SpecialistDto specialist)
        {
            await _service.UpdateSpecialistAsync(specialist);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialist(int id)
        {
            var specialist = await _service.GetSpecialistByIdAsync(id);
            if (specialist == null) return NotFound();
            await _service.DeleteSpecialistAsync(id);
            return NoContent();
        }
    }
}
