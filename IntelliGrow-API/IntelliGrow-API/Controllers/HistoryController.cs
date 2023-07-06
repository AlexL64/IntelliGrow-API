using IntelliGrow_API.Models;
using IntelliGrow_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntelliGrow_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly HistoryService _historyService;

        public HistoryController(HistoryService HistoryService) =>
            _historyService = HistoryService;

        // GET
        [HttpGet]
        public async Task<List<History>> Get() =>
            await _historyService.GetAsync();

        // GET BY ID
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<History>> Get(string id)
        {
            var history = await _historyService.GetAsync(id);

            if (history is null)
            {
                return NotFound();
            }

            return history;
        }

        // GET BY DEVICE
        [HttpGet("Device/{device}")]
        public async Task<List<History>> GetForTask(string device) =>
            await _historyService.GetForDeviceAsync(device);

        // POST
        [HttpPost]
        public async Task<IActionResult> Post(History newHistory)
        {
            await _historyService.CreateAsync(newHistory);

            return CreatedAtAction(nameof(Get), new { id = newHistory.Id }, newHistory);
        }

        // PUT
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, History updatedHistory)
        {
            var history = await _historyService.GetAsync(id);

            if (history is null)
            {
                return NotFound();
            }

            updatedHistory.Id = history.Id;

            await _historyService.UpdateAsync(id, updatedHistory);

            return NoContent();
        }

        // DELETE
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var history = await _historyService.GetAsync(id);

            if (history is null)
            {
                return NotFound();
            }

            await _historyService.RemoveAsync(id);

            return NoContent();
        }
    }
}
