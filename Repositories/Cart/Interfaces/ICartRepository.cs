using Grafitist.Misc;
using Grafitist.Models.Cart;

namespace Grafitist.Repositories.Cart.Interfaces;

public interface ICartRepository
{
    public Task<CartModel?> Get(Guid? id);
    public Task<IEnumerable<CartModel>> GetByUserId(Guid userId, Pager pager);
    public Task<CartModel> Insert(CartModel model);
    public Task Delete(Guid id);
}