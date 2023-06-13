using Grafitist.Authorization;
using Grafitist.Contracts.Payment.Request;
using Grafitist.Misc;
using Grafitist.Services.Payment.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.Controllers;

public class PaymentController : ControllerBase
{
    // todo : user infos must be checked if logged in user or admin
    private readonly IPaymentService _service;

    public PaymentController(IPaymentService service)
    {
        _service = service;
    }

    [HttpGet("{id:Guid}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult?> Get(Guid id)
    {
        return Ok(await _service.Get(id));
    }

    [HttpGet("get-by-order/{orderId:Guid}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> GetByOrder(Guid orderId)
    {
        return Ok(await _service.GetByOrder(orderId));
    }

    [HttpGet("get-by-user/{userId:Guid}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> GetByUser(Guid userId, [FromQuery] int pageNo, [FromQuery] int count, [FromQuery] bool onlyActive)
    {
        return Ok(await _service.Get(userId, new Pager { No = pageNo, Count = count, OnlyActive = onlyActive }));
    }

    [HttpGet("get")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Get([FromQuery] DateTime startTime, [FromQuery] DateTime endTime, [FromQuery] int pageNo, [FromQuery] int count, [FromQuery] bool onlyActive)
    {
        return Ok(await _service.Get(new DateFilter { StartDate = startTime, EndDate = endTime }, new Pager { No = pageNo, Count = count, OnlyActive = onlyActive }));
    }

    [HttpPost]
    [Authorize(Policy = Policies.User)]
    public async Task<IActionResult> Insert(PaymentInsertDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Insert(dto));
    }
}