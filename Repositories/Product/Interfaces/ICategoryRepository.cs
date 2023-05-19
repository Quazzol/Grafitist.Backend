using Grafitist.Models.Product;

namespace Grafitist.Repositories.Product.Interfaces;

public interface ICategoryRepository
{
    public Task<CategoryModel?> Get(int id);
    public Task<IEnumerable<CategoryModel>> Get();
    public Task<CategoryModel> Insert(CategoryModel model);
    public Task<CategoryModel> Update(CategoryModel model);
    public Task Delete(int id);
}