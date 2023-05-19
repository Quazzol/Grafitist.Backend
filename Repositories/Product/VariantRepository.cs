using Grafitist.Connection;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.Product;

public class VariantRepository : IVariantRepository
{
    private readonly AppDbContext _context;

    public VariantRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(int id)
    {
        var variant = await _context.Variants!.FirstOrDefaultAsync(q => q.Id == id);
        if (variant is null)
            return;

        _context.Variants!.Remove(variant);
        await _context.SaveChangesAsync();
    }

    public async Task<VariantModel?> Get(int id)
    {
        return await _context.Variants!.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<VariantModel>> Get()
    {
        return await _context.Variants!.OrderBy(q => q.Id).ToListAsync();
    }

    public async Task<VariantModel> Insert(VariantModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(VariantModel));
        await _context.Variants!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<VariantModel> Update(VariantModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(VariantModel));
        var variant = await _context.Variants!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (variant is null) throw new KeyNotFoundException($"Model.Id not found! {model.Id}");

        variant.Code = model.Code;
        variant.Description = model.Description;
        variant.IsActive = model.IsActive;
        await _context.SaveChangesAsync();
        return variant;
    }
}