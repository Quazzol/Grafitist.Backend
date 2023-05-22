using Grafitist.Models.Order;

namespace Grafitist.Repositories.Order.Interfaces;

public interface IOrderLineRepository
{
    public Task<IEnumerable<OrderLineModel>> Get(Guid orderId);
    public Task<OrderLineModel> Insert(OrderLineModel model);
    public Task<IEnumerable<OrderLineModel>> Insert(IEnumerable<OrderLineModel> models);
}