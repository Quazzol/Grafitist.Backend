using Grafitist.Authorization;
using Grafitist.Contracts.User.Request;
using Grafitist.Misc;
using Grafitist.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet("{id:Guid}")]
    [Authorize(Policy = Policies.HasLoggedIn)]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.Get(id));
    }

    [HttpGet("get")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Get([FromQuery] int pageNo, [FromQuery] int count, [FromQuery] bool onlyActive)
    {
        return Ok(await _service.Get(new Pager { No = pageNo, Count = count, OnlyActive = onlyActive }));
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(UserSignInDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.SignIn(dto));
    }

    [HttpPost()]
    public async Task<IActionResult> Insert(UserInsertDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Insert(dto));
    }

    [HttpPut()]
    [Authorize(Policy = Policies.User)]
    public async Task<IActionResult> Update(UserUpdateDTO dto)
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
}