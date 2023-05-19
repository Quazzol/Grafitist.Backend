using Grafitist.Misc;
using Grafitist.Models.Payment;

namespace Grafitist.Repositories.Payment.Interfaces;

public interface IPaymentRepository
{
    public Task Delete(Guid id);
    public Task<PaymentModel?> Get(Guid id);
    public Task<PaymentModel?> GetByOrder(Guid orderId);
    public Task<IEnumerable<PaymentModel>> Get(Guid userId, Pager pager);
    public Task<IEnumerable<PaymentModel>> Get(DateFilter filter, Pager pager);
    public Task<PaymentModel> Insert(PaymentModel model);
}