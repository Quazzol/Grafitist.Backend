using Grafitist.Authorization;
using Grafitist.Contracts.User.Request;
using Grafitist.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.Controllers;

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
    [Authorize(Policy = Policies.HasLoggedIn)]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _service.Get(id);
        if (response is null)
            return NoContent();
        return Ok(response);
    }

    [HttpGet("get-by-user/{userId:Guid}")]
    [Authorize(Policy = Policies.HasLoggedIn)]
    public async Task<IActionResult> Get(Guid userId)
    {
        var response = await _service.Get(userId);
        if (response is null)
            return NoContent();
        return Ok(response);
    }

    [HttpPost()]
    [Authorize(Policy = Policies.User)]
    public async Task<IActionResult> Insert(AddressInsertDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Insert(dto));
    }

    [HttpPut]
    [Authorize(Policy = Policies.User)]
    public async Task<IActionResult> Update(AddressUpdateDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Update(dto));
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = Policies.User)]
    public async Task Delete(int id)
    {
        await _service.Delete(id);
    }
}