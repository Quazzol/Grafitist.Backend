using Grafitist.Authorization;
using Grafitist.Contracts.Stock.Request;
using Grafitist.Services.Stock.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockService _service;

    public StockController(IStockService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Get(int productId)
    {
        return Ok(await _service.Get(productId));
    }

    [HttpGet("multiple")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Get([FromQuery] int[] productId)
    {
        return Ok(await _service.Get(productId));
    }

    [HttpPost()]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Save(StockQuantityDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.AddStock(dto));
    }
}