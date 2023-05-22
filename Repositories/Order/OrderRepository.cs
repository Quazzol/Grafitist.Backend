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
        return await _context.Orders!.Include(q => q.User).Include(q => q.Cart).Include(q => q.Lines).FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<OrderModel>> Get(Guid userId, Pager pager)
    {
        return await _context.Orders!.Where(q => q.UserId == userId)
                                    .Include(q => q.User)
                                    .Include(q => q.Lines)
                                    .OrderByDescending(q => q.CreatedDate)
                                    .Skip(pager.Count * (pager.No - 1))
                                    .Take(pager.Count).ToListAsync();
    }

    public async Task<IEnumerable<OrderModel>> Get(DateFilter? filter, OrderStatus? status, Pager pager)
    {
        filter = filter ?? new DateFilter();

        return await _context.Orders!.Include(q => q.User)
                                    .Include(q => q.Cart)
                                    .Include(q => q.Lines)
                                    .Where(q => q.CreatedDate >= filter.StartDate
                                            && q.CreatedDate <= filter.EndDate
                                            && (status == null || q.Status == status))
                                    .OrderByDescending(q => q.CreatedDate)
                                    .Skip(pager.Count * (pager.No - 1))
                                    .Take(pager.Count).ToListAsync();
    }

    public async Task<OrderModel> Insert(OrderModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(OrderModel));
        model.OrderNumber = CreateOrderNumber();
        await _context.Orders!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;

        string CreateOrderNumber()
        {
            return string.Format("{0}-{1:00}{2:000}", model.UserId.ToString("N").Substring(0, 5), model.CreatedDate.Second, model.CreatedDate.Millisecond);
        }
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