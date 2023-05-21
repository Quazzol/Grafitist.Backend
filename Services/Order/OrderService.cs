using AutoMapper;
using Grafitist.Contracts.Order.Request;
using Grafitist.Contracts.Order.Response;
using Grafitist.Misc;
using Grafitist.Models.Order;
using Grafitist.Repositories.Order.Interfaces;
using Grafitist.Services.Order.Interfaces;
using Misc.Enums;

namespace Grafitist.Services.Order;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Cancel(Guid id)
    {
        await _repository.Update(new OrderModel { Id = id, Status = OrderStatus.Canceled });
    }

    public async Task<OrderDTO?> Get(Guid id)
    {
        return _mapper.Map<OrderDTO>(await _repository.Get(id));
    }

    public async Task<IEnumerable<OrderDTO>> Get(Guid userId, Pager pager)
    {
        return _mapper.Map<IEnumerable<OrderDTO>>(await _repository.Get(userId, pager));
    }

    public async Task<IEnumerable<OrderDTO>> Get(DateFilter? filter, OrderStatus? status, Pager pager)
    {
        return _mapper.Map<IEnumerable<OrderDTO>>(await _repository.Get(filter, status, pager));
    }

    public async Task<OrderDTO> Insert(OrderInsertDTO model)
    {
        return _mapper.Map<OrderDTO>(await _repository.Insert(_mapper.Map<OrderModel>(model)));
    }

    public async Task<OrderDTO> Update(OrderUpdateDTO model)
    {
        return _mapper.Map<OrderDTO>(await _repository.Update(_mapper.Map<OrderModel>(model)));
    }
}