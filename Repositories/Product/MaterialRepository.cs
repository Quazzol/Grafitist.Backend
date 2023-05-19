using Grafitist.Connection;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.Product;

public class MaterialRepository : IMaterialRepository
{
    private readonly AppDbContext _context;

    public MaterialRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(int id)
    {
        var material = await _context.Materials!.FirstOrDefaultAsync(q => q.Id == id);
        if (material is null)
            return;

        _context.Materials!.Remove(material);
        await _context.SaveChangesAsync();
    }

    public async Task<MaterialModel?> Get(int id)
    {
        return await _context.Materials!.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<MaterialModel>> Get()
    {
        return await _context.Materials!.OrderBy(q => q.Id).ToListAsync();
    }

    public async Task<MaterialModel> Insert(MaterialModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(MaterialModel));
        await _context.Materials!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<MaterialModel> Update(MaterialModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(MaterialModel));
        var material = await _context.Materials!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (material is null) throw new KeyNotFoundException($"Model.Id not found! {model.Id}");

        material.Code = model.Code;
        material.Description = model.Description;
        material.IsActive = model.IsActive;
        await _context.SaveChangesAsync();
        return material;
    }
}