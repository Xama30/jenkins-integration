using Microsoft.AspNetCore.Mvc;
using ServeurTracker.Api.Models;
using ServeurTracker.Api.Services;

namespace ServeurTracker.Api.Controllers
{
    [ApiController] // Tells .NET this is an API controller (handles routing/errors automatically)
    [Route("api/[controller]")] // The route will be: http://localhost:PORT/api/device
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _service;

        // Dependency Injection: The Controller asks for the Service
        public DeviceController(IDeviceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var devices = await _service.GetAllAsync();
            return Ok(devices); // Returns HTTP 200 with the data
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var device = await _service.GetByIdAsync(id);
            if (device == null) return NotFound(); // Returns HTTP 404

            return Ok(device);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Device device)
        {
            await _service.AddAsync(device);
            // Returns HTTP 201 Created, and a link to get the newly created resource
            return CreatedAtAction(nameof(GetById), new { id = device.Id }, device);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Device device)
        {
            try
            {
                await _service.UpdateAsync(id, device);
                return NoContent(); // Returns HTTP 204 (Success, but no data to return)
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}