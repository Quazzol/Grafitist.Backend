using AutoMapper;
using Grafitist.Contracts.Order.Request;
using Grafitist.Contracts.Payment.Request;
using Grafitist.Contracts.Payment.Response;
using Grafitist.Misc;
using Grafitist.Models.Payment;
using Grafitist.Repositories.Payment.Interfaces;
using Grafitist.Services.Order.Interfaces;
using Grafitist.Services.Payment.Interfaces;
using Misc.Enums;

namespace Grafitist.Services.Payment;

public class PaymentService : IPaymentService
{
    //private readonly string Salt = "12345678";
    private readonly IPaymentRepository _repository;
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;

    public PaymentService(IPaymentRepository repository, IMapper mapper, IOrderService orderService)
    {
        _repository = repository;
        _mapper = mapper;
        _orderService = orderService;
    }

    public async Task<PaymentDTO?> Get(Guid id)
    {
        return _mapper.Map<PaymentDTO>(await _repository.Get(id));
    }

    public async Task<IEnumerable<PaymentDTO>> Get(Guid userId, Pager pager)
    {
        return _mapper.Map<IEnumerable<PaymentDTO>>(await _repository.Get(userId, pager));
    }

    public async Task<IEnumerable<PaymentDTO>> Get(DateFilter filter, Pager pager)
    {
        return _mapper.Map<IEnumerable<PaymentDTO>>(await _repository.Get(filter, pager));
    }

    public async Task<PaymentDTO?> GetByOrder(Guid orderId)
    {
        return _mapper.Map<PaymentDTO>(await _repository.Get(orderId));
    }

    public async Task<PaymentDTO> Insert(PaymentInsertDTO model)
    {
        // todo : hash code must be checked
        var payment = _mapper.Map<PaymentDTO>(await _repository.Insert(_mapper.Map<PaymentModel>(model)));
        await _orderService.Update(new OrderUpdateDTO
        {
            Id = model.OrderId,
            Status = OrderStatus.Confirmed
        });
        return payment;
    }
}