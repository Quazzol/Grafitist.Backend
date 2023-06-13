using Grafitist.Authorization;
using Grafitist.Contracts.Product.Request;
using Grafitist.Services.Product.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
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
    public async Task<IActionResult> Insert(CategoryInsertDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Insert(dto));
    }

    [HttpPut]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Update(CategoryUpdateDTO dto)
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