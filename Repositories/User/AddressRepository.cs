using Grafitist.Connection;
using Grafitist.Models.User;
using Grafitist.Repositories.User.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.User;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(int id)
    {
        var address = await _context.Addresses!.FirstOrDefaultAsync(q => q.Id == id);
        if (address is null)
            return;

        _context.Addresses!.Remove(address);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AddressModel?>> Get(Guid userId)
    {
        return await _context.Addresses!.Include(q => q.City).Include(q => q.District).Where(q => q.UserId == userId).ToListAsync();
    }

    public async Task<AddressModel?> Get(int id)
    {
        return await _context.Addresses!.Include(q => q.City).Include(q => q.District).FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<AddressModel> Insert(AddressModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(AddressModel));
        await _context.Addresses!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<AddressModel> Update(AddressModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(UserModel));
        var address = await _context.Addresses!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (address is null) throw new KeyNotFoundException($"Model.Id not found! {model.Id}");

        address.CityId = model.CityId;
        address.DistrictId = model.DistrictId;
        address.Name = model.Name;
        address.OpenAddress = model.OpenAddress;
        await _context.SaveChangesAsync();
        return address;
    }
}