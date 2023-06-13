using Grafitist.Connection;
using Grafitist.Misc;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.Product;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(int id)
    {
        var product = await _context.Products!.FirstOrDefaultAsync(q => q.Id == id);
        if (product is null)
            return;

        _context.Products!.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<ProductModel?> Get(int id)
    {
        return await _context.Products!
                    .Include(q => q.Category)
                    .Include(q => q.Material)
                    .Include(q => q.Color)
                    .Include(q => q.Variant)
                    .Include(q => q.Images)
                    .Include(q => q.Stock)
                    .FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<ProductModel>> Get(Pager? pager, ProductFilter? filter)
    {
        pager = pager ?? new Pager();
        filter = filter ?? new ProductFilter();

        return await _context.Products!
                            .Include(q => q.Category)
                            .Include(q => q.Material)
                            .Include(q => q.Color)
                            .Include(q => q.Variant)
                            .Include(q => q.Images)
                            .Include(q => q.Stock)
                            .OrderBy(q => q.Id)
                            .Where(q => (!pager.OnlyActive || q.IsActive)
                                        && (filter.CategoryId == null || filter.CategoryId.Contains(q.CategoryId))
                                        && (filter.MaterialId == null || filter.MaterialId.Contains(q.MaterialId))
                                        && (filter.ColorId == null || filter.ColorId.Contains(q.ColorId))
                                        && (filter.VariantId == null || filter.VariantId.Contains(q.VariantId))
                                        && (filter.Price == null || (filter.Price.Value.Start <= q.Price && filter.Price.Value.End >= q.Price)))
                            .Skip(pager.Count * (pager.No - 1))
                            .Take(pager.Count)
                            .ToListAsync();
    }

    public async Task<IEnumerable<ProductModel>> Get(IEnumerable<int> productIds)
    {
        return await _context.Products!.Where(q => productIds.Contains(q.Id)).ToListAsync();
    }

    public async Task<int> Get(ProductFilter? filter)
    {
        filter = filter ?? new ProductFilter();
        return await _context.Products!
                    .Where(q => (filter.CategoryId == null || filter.CategoryId.Contains(q.CategoryId))
                                    && (filter.MaterialId == null || filter.MaterialId.Contains(q.MaterialId))
                                    && (filter.ColorId == null || filter.ColorId.Contains(q.ColorId))
                                    && (filter.VariantId == null || filter.VariantId.Contains(q.VariantId))
                                    && (filter.Price == null || (filter.Price.Value.Start <= q.Price && filter.Price.Value.End >= q.Price)))
                    .CountAsync();
    }

    public async Task<ProductModel> Insert(ProductModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(ProductModel));
        await _context.Products!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<ProductModel> Update(ProductModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(ProductModel));
        var product = await _context.Products!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (product is null) throw new KeyNotFoundException($"Model.Id not found! {model.Id}");

        product.Code = model.Code;
        product.Description = model.Description;
        product.Notes = model.Notes;
        product.ColorId = model.ColorId;
        product.VariantId = model.VariantId;
        product.IsActive = model.IsActive;
        product.CategoryId = model.CategoryId;
        product.MaterialId = model.MaterialId;
        product.Price = model.Price;
        await _context.SaveChangesAsync();
        return product;
    }
}