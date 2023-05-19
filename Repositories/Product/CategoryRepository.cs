using Grafitist.Connection;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.Product;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(int id)
    {
        var category = await _context.Categories!.FirstOrDefaultAsync(q => q.Id == id);
        if (category is null)
            return;

        _context.Categories!.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<CategoryModel?> Get(int id)
    {
        return await _context.Categories!.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<CategoryModel>> Get()
    {
        return await _context.Categories!.OrderBy(q => q.Id).ToListAsync();
    }

    public async Task<CategoryModel> Insert(CategoryModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(CategoryModel));
        await _context.Categories!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<CategoryModel> Update(CategoryModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(CategoryModel));
        var category = await _context.Categories!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (category is null) throw new KeyNotFoundException($"Model.Id not found! {model.Id}");

        category.Code = model.Code;
        category.Description = model.Description;
        category.IsActive = model.IsActive;
        await _context.SaveChangesAsync();
        return category;
    }
}