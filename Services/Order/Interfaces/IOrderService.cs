using Grafitist.Contracts.Order.Request;
using Grafitist.Contracts.Order.Response;
using Grafitist.Misc;
using Misc.Enums;

namespace Grafitist.Services.Order.Interfaces;

public interface IOrderService
{
    public Task Cancel(Guid id);
    public Task<OrderDTO?> Get(Guid id);
    public Task<IEnumerable<OrderDTO>> Get(Guid userId, Pager pager);
    public Task<IEnumerable<OrderDTO>> Get(DateFilter? filter, OrderStatus? status, Pager pager);
    public Task<OrderDTO> Insert(OrderInsertDTO model);
    public Task<OrderDTO> Update(OrderUpdateDTO model);
}