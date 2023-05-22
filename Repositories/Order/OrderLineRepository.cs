using Grafitist.Connection;
using Grafitist.Models.Order;
using Grafitist.Repositories.Order.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.Order;

public class OrderLineRepository : IOrderLineRepository
{
    private readonly AppDbContext _context;

    public OrderLineRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderLineModel>> Get(Guid orderId)
    {
        return await _context.OrderLines!.Include(q => q.Campaign).Include(q => q.Product).Where(q => q.OrderId == orderId).OrderBy(q => q.ProductId).ToListAsync();
    }

    public async Task<OrderLineModel> Insert(OrderLineModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(OrderLineModel));
        await _context.OrderLines!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<IEnumerable<OrderLineModel>> Insert(IEnumerable<OrderLineModel> models)
    {
        if (models is null) throw new ArgumentNullException(nameof(OrderLineModel));
        await _context.OrderLines!.AddRangeAsync(models);
        await _context.SaveChangesAsync();
        return models;
    }
}