using Grafitist.Models.Product;

namespace Grafitist.Repositories.Product.Interfaces;

public interface IItemRepository
{
    public Task<ItemModel?> Get(int id);
    public Task<IEnumerable<ItemModel>> Get();
    public Task<ItemModel> Insert(ItemModel model);
    public Task<ItemModel> Update(ItemModel model);
    public Task Delete(int id);
}