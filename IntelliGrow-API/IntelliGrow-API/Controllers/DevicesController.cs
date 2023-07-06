using IntelliGrow_API.Models;
using IntelliGrow_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntelliGrow_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly DevicesService _devicesService;

        public DevicesController(DevicesService DevicesService) =>
            _devicesService = DevicesService;

        // GET
        [HttpGet]
        public async Task<List<Devices>> Get() =>
            await _devicesService.GetAsync();

        // GET BY ID
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Devices>> Get(string id)
        {
            var device = await _devicesService.GetAsync(id);

            if (device is null)
            {
                return NotFound();
            }

            return device;
        }

        // GET BY USER
        [HttpGet("User/{user}")]
        public async Task<List<Devices>> GetForTask(string user) =>
            await _devicesService.GetForUserAsync(user);

        // POST
        [HttpPost]
        public async Task<IActionResult> Post(Devices newDevice)
        {
            await _devicesService.CreateAsync(newDevice);

            return CreatedAtAction(nameof(Get), new { id = newDevice.Id }, newDevice);
        }

        // PUT
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Devices updatedDevice)
        {
            var device = await _devicesService.GetAsync(id);

            if (device is null)
            {
                return NotFound();
            }

            updatedDevice.Id = device.Id;

            await _devicesService.UpdateAsync(id, updatedDevice);

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var device = await _devicesService.GetAsync(id);

            if (device is null)
            {
                return NotFound();
            }

            await _devicesService.RemoveAsync(id);

            return NoContent();
        }
    }
}
