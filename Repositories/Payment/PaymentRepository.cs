using Grafitist.Connection;
using Grafitist.Misc;
using Grafitist.Models.Payment;
using Grafitist.Repositories.Payment.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.Payment;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(Guid id)
    {
        var payment = await _context.Payments!.FirstOrDefaultAsync(q => q.Id == id);
        if (payment is null)
            return;

        _context.Payments!.Remove(payment);
        await _context.SaveChangesAsync();
    }

    public async Task<PaymentModel?> Get(Guid id)
    {
        return await _context.Payments!.Include(q => q.User).FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<PaymentModel?> GetByOrder(Guid orderId)
    {
        return await _context.Payments!.Include(q => q.User).FirstOrDefaultAsync(q => q.OrderId == orderId);
    }

    public async Task<IEnumerable<PaymentModel>> Get(Guid userId, Pager pager)
    {
        return await _context.Payments!.Where(q => q.UserId == userId)
                                        .OrderByDescending(q => q.CreatedDate)
                                        .Include(q => q.User)
                                        .Skip(pager.Count * (pager.No - 1))
                                        .Take(pager.Count).ToListAsync();
    }

    public async Task<IEnumerable<PaymentModel>> Get(DateFilter filter, Pager pager)
    {
        return await _context.Payments!.Where(q => q.CreatedDate >= filter.StartDate && q.CreatedDate <= filter.EndDate)
                                        .OrderByDescending(q => q.CreatedDate)
                                        .Skip(pager.Count * (pager.No - 1))
                                        .Take(pager.Count).ToListAsync();
    }

    public async Task<PaymentModel> Insert(PaymentModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(PaymentModel));
        await _context.Payments!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }
}