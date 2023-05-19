using AutoMapper;
using Grafitist.Contracts.User.Request;
using Grafitist.Contracts.User.Response;
using Grafitist.Misc.Interfaces;
using Grafitist.Models.User;
using Grafitist.Repositories.User.Interfaces;
using Grafitist.Services.User.Interfaces;

namespace Grafitist.Services.User;

public class AddressDataService : IAddressDataService
{
    private readonly IAddressDataRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public AddressDataService(IAddressDataRepository repository, IMapper mapper, IUserContext userContext)
    {
        _repository = repository;
        _mapper = mapper;
        _userContext = userContext;
    }

    public async Task DeleteCity(int id)
    {
        await _repository.DeleteCity(id);
    }

    public async Task DeleteDistrict(int id)
    {
        await _repository.DeleteDistrict(id);
    }

    public async Task<IEnumerable<CityDTO>> GetCities()
    {
        return _mapper.Map<IEnumerable<CityDTO>>(await _repository.GetCities());
    }

    public async Task<IEnumerable<DistrictDTO>> GetDistricts(int cityId)
    {
        return _mapper.Map<IEnumerable<DistrictDTO>>(await _repository.GetDistricts(cityId));
    }

    public async Task<CityDTO> InsertCity(CityInsertDTO dto)
    {
        return _mapper.Map<CityDTO>(await _repository.InsertCity(_mapper.Map<CityModel>(dto)));
    }

    public async Task<DistrictDTO> InsertDistrict(DistrictInsertDTO dto)
    {
        return _mapper.Map<DistrictDTO>(await _repository.InsertDistrict(_mapper.Map<DistrictModel>(dto)));
    }
}
