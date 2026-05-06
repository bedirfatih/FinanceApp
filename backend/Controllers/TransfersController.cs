using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("transfers")]
public class TransfersController(TransfersService transfersService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await transfersService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var transfer = await transfersService.GetByIdAsync(id);
        return transfer is null ? NotFound() : Ok(transfer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransferRequest request)
    {
        var transfer = await transfersService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = transfer.Id }, transfer);
    }
}
