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
    public async Task<IActionResult> Get(int pageNo, int count, bool onlyActive = true, [FromQuery] int[]? categoryId = null, [FromQuery] int[]? materialId = null, [FromQuery] int?[]? colorId = null, [FromQuery] int?[]? variantId = null, [FromQuery] double startPrice = 0, [FromQuery] double endPrice = 0)
    {
        return Ok(await _service.Get(
            new Pager
            {
                No = pageNo,
                Count = count,
                OnlyActive = onlyActive
            },
            new ProductFilter
            {
                CategoryId = categoryId == null || categoryId.Length == 0 ? null : new List<int>(categoryId),
                MaterialId = materialId == null || materialId.Length == 0 ? null : new List<int>(materialId),
                ColorId = colorId == null || colorId.Length == 0 ? null : new List<int?>(colorId),
                VariantId = variantId == null || variantId.Length == 0 ? null : new List<int?>(variantId),
                Price = startPrice.IsEmpty() && endPrice.IsEmpty() ? null : new DoubleRange(startPrice, endPrice)
            }));
    }

    [HttpGet("count")]
    public async Task<IActionResult> Count([FromQuery] int[]? categoryId = null, [FromQuery] int[]? materialId = null, [FromQuery] int?[]? colorId = null, [FromQuery] int?[]? variantId = null, [FromQuery] double startPrice = 0, [FromQuery] double endPrice = 0)
    {
        return Ok(await _service.Get(new ProductFilter
        {
            CategoryId = categoryId == null || categoryId.Length == 0 ? null : new List<int>(categoryId),
            MaterialId = materialId == null || materialId.Length == 0 ? null : new List<int>(materialId),
            ColorId = colorId == null || colorId.Length == 0 ? null : new List<int?>(colorId),
            VariantId = variantId == null || variantId.Length == 0 ? null : new List<int?>(variantId),
            Price = startPrice.IsEmpty() && endPrice.IsEmpty() ? null : new DoubleRange(startPrice, endPrice)
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