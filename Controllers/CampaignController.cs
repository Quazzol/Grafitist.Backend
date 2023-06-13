using Grafitist.Authorization;
using Grafitist.Contracts.Campaign.Request;
using Grafitist.Misc;
using Grafitist.Misc.Enums;
using Grafitist.Services.Campaign.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CampaignController : ControllerBase
{
    private readonly ICampaignService _service;

    public CampaignController(ICampaignService service)
    {
        _service = service;
    }

    [HttpGet("get")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Get([FromQuery] int pageNo, [FromQuery] int count, [FromQuery] bool onlyActive)
    {
        return Ok(await _service.Get(new Pager { No = pageNo, Count = count, OnlyActive = onlyActive }));
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult?> Get(Guid id)
    {
        return Ok(await _service.Get(id));
    }

    [HttpGet("type/{type:int}")]
    public async Task<IActionResult> Get(CampaignType type)
    {
        return Ok(await _service.Get(type));
    }

    [HttpPost()]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Insert(CampaignInsertDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Insert(dto));
    }

    [HttpPut]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Update(CampaignUpdateDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Update(dto));
    }

    // [HttpDelete("{id:Guid}")]
    // [Authorize(Policy = Policies.Admin)]
    // public async Task Delete(Guid id)
    // {
    //     await _service.Delete(id);
    // }

    [HttpGet("deactivate/{id:Guid}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        return Ok(await _service.Deactivate(id));
    }
}