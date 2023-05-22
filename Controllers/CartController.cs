using Grafitist.Contracts.Cart.Request;
using Grafitist.Misc;
using Grafitist.Services.Cart.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.ProductService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }

    [HttpGet("approve/{id:Guid}")]
    public async Task<IActionResult> ApproveCart(Guid id)
    {
        return Ok(await _service.ApproveCart(id));
    }

    [HttpGet("clear/{id:Guid}")]
    public async Task ClearCart(Guid id)
    {
        await _service.ClearCart(id);
    }

    [HttpDelete("{id:Guid}")]
    public async Task DeleteCartLine(Guid id)
    {
        await _service.DeleteLine(id);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.Get(id));
    }

    [HttpGet("get-by-user/{id:Guid}")]
    public async Task<IActionResult> GetByUser(Guid id, [FromQuery] int pageNo, [FromQuery] int count, [FromQuery] bool onlyActive)
    {
        return Ok(await _service.GetByUser(id, new Pager { No = pageNo, Count = count, OnlyActive = onlyActive }));
    }

    [HttpPost()]
    public async Task<IActionResult> Insert(CartLineInsertDTO dto)
    {
        return Ok(await _service.Insert(dto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(CartLineUpdateDTO dto)
    {
        return Ok(await _service.Update(dto));
    }
}