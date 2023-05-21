using Grafitist.Contracts.Cart.Request;
using Grafitist.Contracts.Cart.Response;
using Grafitist.Misc;

namespace Grafitist.Services.Cart.Interfaces;

public interface ICartService
{
    public Task ApproveCart(Guid id);
    public Task ClearCart(Guid id);
    public Task DeleteLine(Guid id);
    public Task<CartDTO> Get(Guid? id);
    public Task<IEnumerable<CartDTO>> GetByUserId(Guid id, Pager pager);
    public Task<CartDTO> InsertLine(CartLineInsertDTO dto);
    public Task<CartDTO> UpdateLine(CartLineUpdateDTO dto);
}