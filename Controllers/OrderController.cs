using Grafitist.Authorization;
using Grafitist.Contracts.Order.Request;
using Grafitist.Misc;
using Grafitist.Services.Order.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Misc.Enums;

namespace Grafitist.Controllers;

[ApiController]
[Route("api/v1/[controller]")]

/* will be removed as orders created and updated through other services*/
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpDelete()]
    public async Task Cancel(Guid id)
    {
        await _service.Cancel(id);
    }

    [HttpGet()]
    [Authorize(Policy = Policies.HasLoggedIn)]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.Get(id));
    }

    [HttpGet("get/{userId:Guid}")]
    [Authorize(Policy = Policies.User)]
    public async Task<IActionResult> Get(Guid userId, [FromQuery] int pageNo, [FromQuery] int count, [FromQuery] bool onlyActive)
    {
        return Ok(await _service.Get(userId, new Pager { No = pageNo, Count = count, OnlyActive = onlyActive }));
    }

    [HttpGet("get-by-stats")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Get([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, OrderStatus? status, [FromQuery] int pageNo, [FromQuery] int count, [FromQuery] bool onlyActive)
    {
        return Ok(await _service.Get(new DateFilter { StartDate = startDate, EndDate = endDate }, status, new Pager { No = pageNo, Count = count, OnlyActive = onlyActive }));
    }

    [HttpPost]
    [Authorize(Policy = Policies.User)]
    public async Task<IActionResult> Insert(OrderInsertDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Insert(dto));
    }

    [HttpPut]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Update(OrderUpdateDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Update(dto));
    }
}