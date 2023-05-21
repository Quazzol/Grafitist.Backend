using Grafitist.Contracts.Order.Request;
using Grafitist.Contracts.Order.Response;
using Grafitist.Misc;
using Grafitist.Services.Order.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Misc.Enums;

namespace Grafitist.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    public async Task Cancel(Guid id)
    {
        await _service.Cancel(id);
    }

    [HttpGet()]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.Get(id));
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get([FromQuery] Guid userId, [FromQuery] int pageNo, [FromQuery] int count, [FromQuery] bool onlyActive)
    {
        return Ok(await _service.Get(userId, new Pager { No = pageNo, Count = count, OnlyActive = onlyActive }));
    }

    [HttpGet("get-by-stats")]
    public async Task<IActionResult> Get(DateTime startDate, DateTime endDate, OrderStatus? status, [FromQuery] int pageNo, [FromQuery] int count, [FromQuery] bool onlyActive)
    {
        return Ok(await _service.Get(new DateFilter { StartDate = startDate, EndDate = endDate }, status, new Pager { No = pageNo, Count = count, OnlyActive = onlyActive }));
    }

    public async Task<IActionResult> Insert(OrderInsertDTO model)
    {
        return Ok(await _service.Insert(model));
    }

    public async Task<IActionResult> Update(OrderUpdateDTO model)
    {
        return Ok(await _service.Update(model));
    }
}