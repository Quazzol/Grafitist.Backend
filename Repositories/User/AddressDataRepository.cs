using Grafitist.Connection;
using Grafitist.Models.User;
using Grafitist.Repositories.User.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.User;

public class AddressDataRepository : IAddressDataRepository
{
    private readonly AppDbContext _context;

    public AddressDataRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task DeleteCity(int id)
    {
        var city = await _context.Cities!.FirstOrDefaultAsync(q => q.Id == id);
        if (city is null)
            return;

        _context.Cities!.Remove(city);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDistrict(int id)
    {
        var district = await _context.Districts!.FirstOrDefaultAsync(q => q.Id == id);
        if (district is null)
            return;

        _context.Districts!.Remove(district);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CityModel>> GetCities()
    {
        return await _context.Cities!.OrderBy(q => q.Id).ToListAsync();
    }

    public async Task<IEnumerable<DistrictModel>> GetDistricts(int cityId)
    {
        return await _context.Districts!.Where(q => q.CityId == cityId).OrderBy(q => q.Id).ToListAsync();
    }

    public async Task<CityModel> InsertCity(CityModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(CityModel));
        await _context.Cities!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<DistrictModel> InsertDistrict(DistrictModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(DistrictModel));
        await _context.Districts!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }
}