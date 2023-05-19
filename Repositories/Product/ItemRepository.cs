using Grafitist.Connection;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.Product;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;

    public ItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(int id)
    {
        var item = await _context.Items!.FirstOrDefaultAsync(q => q.Id == id);
        if (item is null)
            return;

        _context.Items!.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task<ItemModel?> Get(int id)
    {
        return await _context.Items!.Include(q => q.Category).Include(q => q.Material).FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<ItemModel>> Get()
    {
        return await _context.Items!.Include(q => q.Category).Include(q => q.Material).OrderBy(q => q.Id).ToListAsync();
    }

    public async Task<ItemModel> Insert(ItemModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(ItemModel));
        await _context.Items!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<ItemModel> Update(ItemModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(ItemModel));
        var item = await _context.Items!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (item is null) throw new KeyNotFoundException($"Model.Id not found! {model.Id}");

        item.Code = model.Code;
        item.Description = model.Description;
        item.IsActive = model.IsActive;
        item.CategoryId = model.CategoryId;
        item.MaterialId = model.MaterialId;
        await _context.SaveChangesAsync();
        return item;
    }
}