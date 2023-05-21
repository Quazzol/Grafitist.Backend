using Grafitist.Misc;
using Grafitist.Models.Order;
using Misc.Enums;

namespace Grafitist.Repositories.Order.Interfaces;

public interface IOrderLineRepository
{
    public Task Delete(Guid id);
    public Task<OrderModel?> Get(Guid id);
    public Task<IEnumerable<OrderModel>> Get(Guid userId, Pager pager);
    public Task<IEnumerable<OrderModel>> Get(DateFilter? filter, OrderStatus? status, Pager pager);
    public Task<OrderModel> Insert(OrderModel model);
    public Task<OrderModel> Update(OrderModel model);
}