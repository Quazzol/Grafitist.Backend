using Grafitist.Connection;
using Grafitist.Misc;
using Grafitist.Models.Cart;
using Grafitist.Repositories.Cart.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.Cart;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;

    public CartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(Guid id)
    {
        var cart = await _context.Carts!.FirstOrDefaultAsync(q => q.Id == id);
        if (cart is null)
            return;

        _context.Carts!.Remove(cart);
        await _context.SaveChangesAsync();
    }

    public async Task<CartModel?> Get(Guid? id)
    {
        if (id.IsEmpty())
            return null;
        return await _context.Carts!.Include(q => q.Lines).FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<CartModel>> GetByUser(Guid userId, Pager pager)
    {
        return await _context.Carts!.Include(q => q.Lines)
                                    .Where(q => q.UserId == userId)
                                    .Skip(pager.Count * (pager.No - 1))
                                    .Take(pager.Count).ToListAsync();
    }

    public async Task<CartModel> Insert(CartModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(CartModel));
        await _context.Carts!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<CartModel> Update(CartModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(CartModel));
        var cart = await _context.Carts!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (cart is null) throw new Exception($"Model.Id not found! {model.Id}");

        cart.Status = model.Status;
        await _context.SaveChangesAsync();
        return cart;
    }
}