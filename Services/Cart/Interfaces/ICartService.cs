using Grafitist.Contracts.Cart.Request;
using Grafitist.Contracts.Cart.Response;
using Grafitist.Misc;

namespace Grafitist.Services.Cart.Interfaces;

public interface ICartService
{
    public Task<CartDTO> ApproveCart(Guid id);
    public Task ClearCart(Guid id);
    public Task DeleteLine(Guid id);
    public Task<CartDTO> Get(Guid? id);
    public Task<IEnumerable<CartDTO>> GetByUser(Guid id, Pager pager);
    public Task<CartDTO> Insert(CartLineInsertDTO dto);
    public Task<CartDTO> Update(CartLineUpdateDTO dto);
}