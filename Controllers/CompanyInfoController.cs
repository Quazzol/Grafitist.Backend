using Grafitist.Authorization;
using Grafitist.Contracts.CompanyInfo.Request;
using Grafitist.Services.CompanyInfo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CompanyInfoController : ControllerBase
{
    private readonly ICompanyInfoService _service;

    public CompanyInfoController(ICompanyInfoService service)
    {
        _service = service;
    }

    [HttpGet()]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.Get());
    }

    [HttpPut]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> Update(CompanyInfoUpdateDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.Update(dto));
    }
}