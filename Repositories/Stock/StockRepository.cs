using Grafitist.Connection;
using Grafitist.Models.Stock;
using Grafitist.Repositories.Stock.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.Stock;

public class StockRepository : IStockRepository
{
    private readonly AppDbContext _context;

    public StockRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(Guid id)
    {
        var stock = await _context.Stocks!.FirstOrDefaultAsync(q => q.Id == id);
        if (stock is null)
            return;

        _context.Stocks!.Remove(stock);
        await _context.SaveChangesAsync();
    }

    public async Task<StockModel?> Get(int productId)
    {
        return await _context.Stocks!.FirstOrDefaultAsync(q => q.ProductId == productId);
    }

    public async Task<IEnumerable<StockModel>> Get(int[] productIds)
    {
        return await _context.Stocks!.Where(q => productIds.Contains(q.ProductId)).ToListAsync();
    }

    public async Task<StockModel> Insert(StockModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(StockModel));
        await _context.Stocks!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<StockModel> Update(StockModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(StockModel));
        var stock = await _context.Stocks!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (stock is null) throw new KeyNotFoundException($"Model.Id not found! {model.Id}");

        stock.Quantity = model.Quantity;
        stock.ReserveQty = model.ReserveQty;
        await _context.SaveChangesAsync();
        return stock;
    }
}