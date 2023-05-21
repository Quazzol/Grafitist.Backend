using Grafitist.Contracts.Payment.Request;
using Grafitist.Misc;
using Grafitist.Services.Payment.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.Controllers;

public class PaymentController : ControllerBase
{
    private readonly IPaymentService _service;

    public PaymentController(IPaymentService service)
    {
        _service = service;
    }

    public async Task<IActionResult?> Get(Guid id)
    {
        return Ok(await _service.Get(id));
    }

    public async Task<IActionResult> GetByOrder(Guid orderId)
    {
        return Ok(await _service.GetByOrder(orderId));
    }

    public async Task<IActionResult> Get(Guid userId, [FromQuery] int pageNo, [FromQuery] int count, [FromQuery] bool onlyActive)
    {
        return Ok(await _service.Get(userId, new Pager { No = pageNo, Count = count, OnlyActive = onlyActive }));
    }

    public async Task<IActionResult> Get(DateTime startTime, DateTime endTime, [FromQuery] int pageNo, [FromQuery] int count, [FromQuery] bool onlyActive)
    {
        return Ok(await _service.Get(new DateFilter { StartDate = startTime, EndDate = endTime }, new Pager { No = pageNo, Count = count, OnlyActive = onlyActive }));
    }

    public async Task<IActionResult> Insert(PaymentInsertDTO model)
    {
        return Ok(await _service.Insert(model));
    }
}