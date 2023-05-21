using Grafitist.Connection;
using Grafitist.Misc;
using Grafitist.Models.Order;
using Grafitist.Repositories.Order.Interfaces;
using Microsoft.EntityFrameworkCore;
using Misc.Enums;

namespace Grafitist.Repositories.Order;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(Guid id)
    {
        var order = await _context.Orders!.FirstOrDefaultAsync(q => q.Id == id);
        if (order == null)
            return;

        _context.Orders!.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<OrderModel?> Get(Guid id)
    {
        return await _context.Orders!.Include(q => q.User).Include(q => q.Cart).FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<OrderModel>> Get(Guid userId, Pager pager)
    {
        return await _context.Orders!.Where(q => q.UserId == userId).Include(q => q.User).Skip(pager.Count * (pager.No - 1)).Take(pager.Count).ToListAsync();
    }

    public async Task<IEnumerable<OrderModel>> Get(DateFilter? filter, OrderStatus? status, Pager pager)
    {
        filter = filter ?? new DateFilter();

        return await _context.Orders!.Include(q => q.User)
                                    .Where(q => q.CreatedDate >= filter.StartDate
                                            && q.CreatedDate <= filter.EndDate
                                            && (status == null || q.Status == status))
                                    .Skip(pager.Count * (pager.No - 1))
                                    .Take(pager.Count).ToListAsync();
    }

    public async Task<OrderModel> Insert(OrderModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(OrderModel));
        await _context.Orders!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<OrderModel> Update(OrderModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(OrderModel));
        var order = await _context.Orders!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (order is null) throw new KeyNotFoundException($"Model.Id not found! {model.Id}");

        order.Status = model.Status;
        await _context.SaveChangesAsync();
        return order;
    }
}