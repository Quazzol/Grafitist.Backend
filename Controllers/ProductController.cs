using Grafitist.Contracts.Product.Request;
using Grafitist.Misc;
using Grafitist.Services.Product.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Grafitist.ProductService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.Get(id));
    }

    [HttpGet()]
    public async Task<IActionResult> Get(int pageNo, int productCount, bool onlyActive = true, int? categoryId = null, int? materialId = null, int? colorId = null, int? variantId = null, double startPrice = 0, double endPrice = double.MaxValue)
    {
        return Ok(await _service.Get(
            new Pager
            {
                No = pageNo,
                Count = productCount,
                OnlyActive = onlyActive
            },
            new ProductFilter
            {
                CategoryId = categoryId,
                MaterialId = materialId,
                ColorId = colorId,
                VariantId = variantId,
                Price = new DoubleRange(startPrice, endPrice)
            }));
    }

    [HttpPost()]
    public async Task<IActionResult> Insert(ProductInsertDTO dto)
    {
        return Ok(await _service.Insert(dto));
    }

    [HttpPut]
    public async Task<IActionResult> Update(ProductUpdateDTO dto)
    {
        return Ok(await _service.Update(dto));
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        await _service.Delete(id);
    }
}