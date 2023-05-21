using Grafitist.Contracts.Payment.Request;
using Grafitist.Contracts.Payment.Response;
using Grafitist.Misc;

namespace Grafitist.Services.Payment.Interfaces;

public interface IPaymentService
{
    public Task<PaymentDTO?> Get(Guid id);
    public Task<PaymentDTO?> GetByOrder(Guid orderId);
    public Task<IEnumerable<PaymentDTO>> Get(Guid userId, Pager pager);
    public Task<IEnumerable<PaymentDTO>> Get(DateFilter filter, Pager pager);
    public Task<PaymentDTO> Insert(PaymentInsertDTO model);
}