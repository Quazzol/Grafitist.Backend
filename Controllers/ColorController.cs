using Grafitist.Authorization;
using Grafitist.Contracts.Product.Request;
using Grafitist.Services.Product.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ColorController : ControllerBase
{
    private readonly IColorService _service;

    public ColorController(IColorService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.Get(id));
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.Get());
    }

    [HttpPost()]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Insert(ColorInsertDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Insert(dto));
    }

    [HttpPut]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Update(ColorUpdateDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Update(dto));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = Policies.Admin)]
    public async Task Delete(int id)
    {
        await _service.Delete(id);
    }
}