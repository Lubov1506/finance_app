using Microsoft.AspNetCore.Mvc;
using mywebapp.api.Models;
using mywebapp.api.Services;

namespace mywebapp.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionsService _service;

        public TransactionsController(TransactionsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<Transaction>> Get() =>
            await _service.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> Get(string id)
        {
            var transaction = await _service.GetAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            await _service.CreateAsync(transaction);
            return CreatedAtAction(nameof(Get), new { id = transaction.Id }, transaction);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Transaction transaction)
        {
            var existing = await _service.GetAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            transaction.Id = id;
            await _service.UpdateAsync(id, transaction);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _service.GetAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
