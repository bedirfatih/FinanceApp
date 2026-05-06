using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionsController(TransactionsService transactionsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await transactionsService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var transaction = await transactionsService.GetByIdAsync(id);
        return transaction is null ? NotFound() : Ok(transaction);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(int userId) =>
        Ok(await transactionsService.GetByUserIdAsync(userId));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionRequest request)
    {
        var transaction = await transactionsService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTransactionRequest request)
    {
        var transaction = await transactionsService.UpdateAsync(id, request);
        return transaction is null ? NotFound() : Ok(transaction);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await transactionsService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
