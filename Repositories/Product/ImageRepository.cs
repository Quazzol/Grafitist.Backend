using Grafitist.Connection;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.Product;

public class ImageRepository : IImageRepository
{
    private readonly AppDbContext _context;

    public ImageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(int id)
    {
        var image = await _context.Images!.FirstOrDefaultAsync(q => q.Id == id);
        if (image is null)
            return;

        _context.Images!.Remove(image);
        await _context.SaveChangesAsync();
    }

    public async Task<ImageModel?> Get(int id)
    {
        return await _context.Images!.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<ImageModel>> GetByProduct(int productId)
    {
        return await _context.Images!.Where(q => q.ProductId == productId).OrderBy(q => q.Id).ToListAsync();
    }

    public async Task<ImageModel> Insert(ImageModel model, string name)
    {
        if (model is null) throw new ArgumentNullException(nameof(ImageModel));
        model.Name = name;
        await _context.Images!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<ImageModel> Update(ImageModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(ImageModel));
        var image = await _context.Images!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (image is null) throw new KeyNotFoundException($"Model.Id not found! {model.Id}");

        image.Name = model.Name;
        image.IsCover = model.IsCover;
        image.IsActive = model.IsActive;
        await _context.SaveChangesAsync();
        return image;
    }
}