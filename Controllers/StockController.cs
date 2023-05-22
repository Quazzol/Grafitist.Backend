using Grafitist.Contracts.Stock.Request;
using Grafitist.Services.Stock.Interfaces;
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
    public async Task<IActionResult> Get(int productId)
    {
        return Ok(await _service.Get(productId));
    }

    [HttpGet("multiple")]
    public async Task<IActionResult> Get([FromQuery] int[] productId)
    {
        return Ok(await _service.Get(productId));
    }

    [HttpPost()]
    public async Task<IActionResult> Save(StockUpdateDTO dto)
    {
        return Ok(await _service.Save(dto));
    }
}