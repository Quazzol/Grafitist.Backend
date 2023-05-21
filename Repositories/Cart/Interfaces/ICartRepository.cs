using Grafitist.Misc;
using Grafitist.Models.Cart;

namespace Grafitist.Repositories.Cart.Interfaces;

public interface ICartRepository
{
    public Task Delete(Guid id);
    public Task<CartModel?> Get(Guid? id);
    public Task<IEnumerable<CartModel>> GetByUser(Guid userId, Pager pager);
    public Task<CartModel> Insert(CartModel model);
    public Task<CartModel> Update(CartModel model);
}