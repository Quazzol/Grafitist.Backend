using Grafitist.Misc;
using Grafitist.Models.Product;

namespace Grafitist.Repositories.Product.Interfaces;

public interface IProductRepository
{
    public Task<ProductModel?> Get(int id);
    public Task<IEnumerable<ProductModel>> Get(Pager? page, ProductFilter? filter);
    public Task<ProductModel> Insert(ProductModel model);
    public Task<ProductModel> Update(ProductModel model);
    public Task Delete(int id);
}