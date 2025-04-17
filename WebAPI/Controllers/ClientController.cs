using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace TheraFlow_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<ClientDto>> GetClients()
        {
            return await _clientService.GetAllClientsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            return client == null ? NotFound() : Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> PostClient([FromForm] ClientDto client)
        {
            try
            {
                await _clientService.AddClientAsync(client);;
                return CreatedAtAction(nameof(GetClient), new { id = client }, client);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding client", ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutClient([FromForm] ClientDto client)
        {
            try
            {
                await _clientService.UpdateClientAsync(client);
                return CreatedAtAction(nameof(GetClient), new { id = client }, client);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding client", ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null) return NotFound();
            await _clientService.DeleteClientAsync(id);
            return NoContent();
        }
    }
}
