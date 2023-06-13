using AutoMapper;
using Grafitist.Contracts.Order.Request;
using Grafitist.Contracts.Order.Response;
using Grafitist.Contracts.Stock.Request;
using Grafitist.Misc;
using Grafitist.Misc.Interfaces;
using Grafitist.Models.Order;
using Grafitist.Repositories.Order.Interfaces;
using Grafitist.Services.Order.Interfaces;
using Grafitist.Services.Stock.Interfaces;
using Misc.Enums;

namespace Grafitist.Services.Order;

public class OrderService : IOrderService
{
    // todo : user infos must be checked if logged in user or admin
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderLineRepository _orderLineRepository;
    private readonly IMapper _mapper;
    private readonly IPriceManager _priceManager;
    private readonly IStockService _stockService;

    public OrderService(IOrderRepository orderRepository, IOrderLineRepository orderLineRepository, IMapper mapper, IPriceManager priceManager, IStockService stockService)
    {
        _orderRepository = orderRepository;
        _orderLineRepository = orderLineRepository;
        _mapper = mapper;
        _priceManager = priceManager;
        _stockService = stockService;
    }

    public async Task Cancel(Guid id)
    {
        var orderDetail = await _orderRepository.Update(new OrderModel { Id = id, Status = OrderStatus.Canceled });
        var stocks = new List<StockQuantityDTO>();
        foreach (var line in orderDetail.Lines!)
        {
            stocks.Add(new StockQuantityDTO
            {
                ProductId = line.ProductId,
                Quantity = line.Quantity
            });
        }
        await _stockService.Unreserve(stocks);
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
        var orderLines = await _orderLineRepository.Insert(_mapper.Map<IEnumerable<OrderLineModel>>(model.Lines));
        var stocks = new List<StockQuantityDTO>();
        foreach (var line in orderLines)
        {
            stocks.Add(new StockQuantityDTO
            {
                ProductId = line.ProductId,
                Quantity = line.Quantity
            });
        }
        await _stockService.Unreserve(stocks);
        return _mapper.Map<OrderDTO>(await _orderRepository.Insert(_mapper.Map<OrderModel>(model))); ;
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