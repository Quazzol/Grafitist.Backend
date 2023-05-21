using AutoMapper;
using Grafitist.Contracts.Payment.Request;
using Grafitist.Contracts.Payment.Response;
using Grafitist.Misc;
using Grafitist.Models.Payment;
using Grafitist.Repositories.Payment.Interfaces;
using Grafitist.Services.Payment.Interfaces;

namespace Grafitist.Services.Payment;

public class PaymentService : IPaymentService
{
    //private readonly string Salt = "12345678";
    private readonly IPaymentRepository _repository;
    private readonly IMapper _mapper;

    public PaymentService(IPaymentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
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
        return _mapper.Map<PaymentDTO>(await _repository.Insert(_mapper.Map<PaymentModel>(model)));
    }
}