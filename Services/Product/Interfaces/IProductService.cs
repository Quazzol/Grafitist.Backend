using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;
using Grafitist.Misc;

namespace Grafitist.Services.Product.Interfaces;

public interface IProductService
{
    public Task<ProductDTO?> Get(int id);
    public Task<IEnumerable<ProductDTO>> Get(Pager? pager, ProductFilter? filter);
    public Task<ProductDTO> Insert(ProductInsertDTO dto);
    public Task<ProductDTO> Update(ProductUpdateDTO dto);
    public Task Delete(int id);
    public Task Deactivate(int id);
}