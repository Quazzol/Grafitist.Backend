using Grafitist.Contracts.Product.Request;
using Grafitist.Services.Product.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.ProductService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageService _service;

    public ImageController(IImageService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.Get(id));
    }

    [HttpGet("get-by-product/{productId:int}")]
    public async Task<IActionResult> GetByProduct(int productId)
    {
        return Ok(await _service.GetByProduct(productId));
    }

    [HttpPost()]
    public async Task<IActionResult> Insert([FromForm] ImageInsertDTO dto)
    {
        return Ok(await _service.Insert(dto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ImageUpdateDTO dto)
    {
        return Ok(await _service.Update(dto));
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        await _service.Delete(id);
    }
}