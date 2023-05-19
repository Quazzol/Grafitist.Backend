using Grafitist.Contracts.User.Request;
using Grafitist.Contracts.User.Response;

namespace Grafitist.Services.User.Interfaces;

public interface IAddressDataService
{
    public Task<IEnumerable<CityDTO>> GetCities();
    public Task<CityDTO> InsertCity(CityInsertDTO dto);
    public Task DeleteCity(int id);
    public Task<IEnumerable<DistrictDTO>> GetDistricts(int cityId);
    public Task<DistrictDTO> InsertDistrict(DistrictInsertDTO dto);
    public Task DeleteDistrict(int id);
}