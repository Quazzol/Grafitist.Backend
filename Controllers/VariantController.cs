using Grafitist.Contracts.Product.Request;
using Grafitist.Services.Product.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.VariantService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VariantController : ControllerBase
{
    private readonly IVariantService _service;

    public VariantController(IVariantService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
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
    public async Task<IActionResult> Insert(VariantInsertDTO dto)
    {
        return Ok(await _service.Insert(dto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(VariantUpdateDTO dto)
    {
        return Ok(await _service.Update(dto));
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _service.Delete(id);
    }
}