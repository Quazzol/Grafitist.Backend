using AutoMapper;
using Grafitist.Contracts.Order.Request;
using Grafitist.Contracts.Order.Response;
using Grafitist.Misc;
using Grafitist.Misc.Interfaces;
using Grafitist.Models.Order;
using Grafitist.Repositories.Order.Interfaces;
using Grafitist.Services.Order.Interfaces;
using Misc.Enums;

namespace Grafitist.Services.Order;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderLineRepository _orderLineRepository;
    private readonly IMapper _mapper;
    private readonly IPriceManager _priceManager;

    public OrderService(IOrderRepository orderRepository, IOrderLineRepository orderLineRepository, IMapper mapper, IPriceManager priceManager)
    {
        _orderRepository = orderRepository;
        _orderLineRepository = orderLineRepository;
        _mapper = mapper;
        _priceManager = priceManager;
    }

    public async Task Cancel(Guid id)
    {
        await _orderRepository.Update(new OrderModel { Id = id, Status = OrderStatus.Canceled });
    }

    public async Task<OrderDTO?> Get(Guid id)
    {
        var order = _mapper.Map<OrderDTO>(await _orderRepository.Get(id));
        await CalculateAmounts(order);
        return order;
    }

    public async Task<IEnumerable<OrderDTO>> Get(Guid userId, Pager pager)
    {
        var orders = _mapper.Map<IEnumerable<OrderDTO>>(await _orderRepository.Get(userId, pager));
        foreach (var order in orders)
        {
            await CalculateAmounts(order);
        }
        return orders;
    }

    public async Task<IEnumerable<OrderDTO>> Get(DateFilter? filter, OrderStatus? status, Pager pager)
    {
        var orders = _mapper.Map<IEnumerable<OrderDTO>>(await _orderRepository.Get(filter, status, pager));
        foreach (var order in orders)
        {
            await CalculateAmounts(order);
        }
        return orders;
    }

    public async Task<OrderDTO> Insert(OrderInsertDTO model)
    {
        await _orderLineRepository.Insert(_mapper.Map<IEnumerable<OrderLineModel>>(model.Lines));
        return _mapper.Map<OrderDTO>(await _orderRepository.Insert(_mapper.Map<OrderModel>(model)));
    }

    public async Task<OrderDTO> Update(OrderUpdateDTO model)
    {
        return _mapper.Map<OrderDTO>(await _orderRepository.Update(_mapper.Map<OrderModel>(model)));
    }

    private async Task CalculateAmounts(OrderDTO order)
    {
        await _priceManager.SetDiscountedPrice(order.Lines);
        if (order.Lines is not null)
        {
            order.Amount = await _priceManager.CalculateTotalDiscountedPrice(order.Lines.Sum(q => q.Amount * q.Quantity));
        }
    }
}