using Grafitist.Connection;
using Grafitist.Models.Cart;
using Grafitist.Repositories.Cart.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.Cart;

public class CartLineRepository : ICartLineRepository
{
    private readonly AppDbContext _context;

    public CartLineRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(Guid id)
    {
        var cartLine = await _context.CartLines!.FirstOrDefaultAsync(q => q.Id == id);
        if (cartLine is null)
            return;

        _context.CartLines!.Remove(cartLine);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByCartId(Guid id)
    {
        var cartLines = await _context.CartLines!.Where(q => q.CartId == id).ToListAsync();
        if (cartLines is null || cartLines.Count == 0)
            return;

        _context.CartLines!.RemoveRange(cartLines);
        await _context.SaveChangesAsync();
    }

    public async Task<CartLineModel> Insert(CartLineModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(CartLineModel));
        await _context.CartLines!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<CartLineModel> Update(CartLineModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(CartLineModel));
        var cartLine = await _context.CartLines!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (cartLine is null) throw new KeyNotFoundException($"Model.Id not found! {model.Id}");

        cartLine.ProductId = model.ProductId;
        cartLine.Quantity = model.Quantity;
        await _context.SaveChangesAsync();
        return cartLine;
    }
}