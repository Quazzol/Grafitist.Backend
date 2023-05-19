using Grafitist.Connection;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.Product;

public class ColorRepository : IColorRepository
{
    private readonly AppDbContext _context;

    public ColorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(int id)
    {
        var color = await _context.Colors!.FirstOrDefaultAsync(q => q.Id == id);
        if (color is null)
            return;

        _context.Colors!.Remove(color);
        await _context.SaveChangesAsync();
    }

    public async Task<ColorModel?> Get(int id)
    {
        return await _context.Colors!.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<ColorModel>> Get()
    {
        return await _context.Colors!.OrderBy(q => q.Id).ToListAsync();
    }

    public async Task<ColorModel> Insert(ColorModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(ColorModel));
        await _context.Colors!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<ColorModel> Update(ColorModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(ColorModel));
        var color = await _context.Colors!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (color is null) throw new KeyNotFoundException($"Model.Id not found! {model.Id}");

        color.Code = model.Code;
        color.Description = model.Description;
        color.HexCode = model.HexCode;
        color.IsActive = model.IsActive;
        await _context.SaveChangesAsync();
        return color;
    }
}