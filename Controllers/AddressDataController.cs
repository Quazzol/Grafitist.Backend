using Grafitist.Authorization;
using Grafitist.Contracts.User.Request;
using Grafitist.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AddressDataController : ControllerBase
{
    private readonly IAddressDataService _service;

    public AddressDataController(IAddressDataService service)
    {
        _service = service;
    }

    [HttpGet("district/{id:int}")]
    public async Task<IActionResult> GetDistricts(int cityId)
    {
        return Ok(await _service.GetDistricts(cityId));
    }

    [HttpGet("city")]
    public async Task<IActionResult> GetCities()
    {
        return Ok(await _service.GetCities());
    }

    [HttpPost("city")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> InsertCity(CityInsertDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.InsertCity(dto));
    }

    [HttpPost("district")]
    [Authorize(Policy = Policies.Admin)]
    public async Task<IActionResult> InsertDistrict(DistrictInsertDTO dto)
    {
        if (!TryValidateModel(dto))
            return ValidationProblem(ModelState);
        return Ok(await _service.InsertDistrict(dto));
    }

    // [HttpDelete("city/{cityId:int}")]
    // [Authorize(Policy = Policies.Admin)]
    // public async Task DeleteCity(int cityId)
    // {
    //     await _service.DeleteCity(cityId);
    // }

    // [HttpDelete("district/{districtId:int}")]
    // [Authorize(Policy = Policies.Admin)]
    // public async Task DeleteDistrict(int districtId)
    // {
    //     await _service.DeleteDistrict(districtId);
    // }
}