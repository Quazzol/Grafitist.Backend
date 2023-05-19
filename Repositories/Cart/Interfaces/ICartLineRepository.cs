using Grafitist.Models.Cart;

namespace Grafitist.Repositories.Cart.Interfaces;

public interface ICartLineRepository
{
    public Task<CartLineModel> Insert(CartLineModel model);
    public Task<CartLineModel> Update(CartLineModel model);
    public Task Delete(Guid id);
    public Task DeleteByCartId(Guid id);
}