using Grafitist.Contracts.User.Request;
using Grafitist.Services.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.ProductService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _service;

    public AddressController(IAddressService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.Get(id));
    }

    [HttpGet("get-by-user/{userId}")]
    public async Task<IActionResult> Get(Guid userId)
    {
        return Ok(await _service.Get(userId));
    }

    [HttpPost()]
    public async Task<IActionResult> Insert(AddressInsertDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Insert(dto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(AddressUpdateDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Update(dto));
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await _service.Delete(id);
    }
}