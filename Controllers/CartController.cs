using Grafitist.Authorization;
using Grafitist.Contracts.Cart.Request;
using Grafitist.Misc;
using Grafitist.Services.Cart.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CartController : ControllerBase
{
    // todo : user infos must be checked if logged in user or admin
    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }

    [HttpGet("approve/{id:Guid}")]
    [Authorize(Policy = Policies.User)]
    public async Task<IActionResult> ApproveCart(Guid id)
    {
        return Ok(await _service.ApproveCart(id));
    }

    [HttpGet("clear/{id:Guid}")]
    [Authorize(Policy = Policies.User)]
    public async Task ClearCart(Guid id)
    {
        await _service.ClearCart(id);
    }

    [HttpDelete("{id:Guid}")]
    [Authorize(Policy = Policies.User)]
    public async Task DeleteCartLine(Guid id)
    {
        await _service.DeleteLine(id);
    }

    [HttpGet("{id:Guid}")]
    [Authorize(Policy = Policies.User)]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.Get(id));
    }

    [HttpGet("get-by-user/{id:Guid}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> GetByUser(Guid id, [FromQuery] int pageNo, [FromQuery] int count, [FromQuery] bool onlyActive)
    {
        return Ok(await _service.GetByUser(id, new Pager { No = pageNo, Count = count, OnlyActive = onlyActive }));
    }

    [HttpPost()]
    [Authorize(Policy = Policies.User)]
    public async Task<IActionResult> Insert(CartLineInsertDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Insert(dto));
    }

    [HttpPut]
    [Authorize(Policy = Policies.User)]
    public async Task<IActionResult> Update(CartLineUpdateDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Update(dto));
    }
}