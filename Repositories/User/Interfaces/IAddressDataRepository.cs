using Grafitist.Models.User;

namespace Grafitist.Repositories.User.Interfaces;

public interface IAddressDataRepository
{
    public Task<IEnumerable<CityModel>> GetCities();
    public Task<CityModel> InsertCity(CityModel model);
    public Task DeleteCity(int id);
    public Task<IEnumerable<DistrictModel>> GetDistricts(int cityId);
    public Task<DistrictModel> InsertDistrict(DistrictModel model);
    public Task DeleteDistrict(int id);
}