using AutoMapper;
using Grafitist.Contracts.User.Request;
using Grafitist.Contracts.User.Response;
using Grafitist.Misc.Interfaces;
using Grafitist.Models.User;
using Grafitist.Repositories.User.Interfaces;
using Grafitist.Services.User.Interfaces;

namespace Grafitist.Services.User;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public AddressService(IAddressRepository repository, IMapper mapper, IUserContext userContext)
    {
        _repository = repository;
        _mapper = mapper;
        _userContext = userContext;
    }
    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }

    public async Task<IEnumerable<AddressDTO?>> Get(Guid userId)
    {
        return _mapper.Map<IEnumerable<AddressDTO>>(await _repository.Get(userId));
    }

    public async Task<AddressDTO?> Get(int id)
    {
        return _mapper.Map<AddressDTO>(await _repository.Get(id));
    }

    public async Task<AddressDTO> Insert(AddressInsertDTO dto)
    {
        return _mapper.Map<AddressDTO>(await _repository.Insert(_mapper.Map<AddressModel>(dto)));
    }

    public async Task<AddressDTO> Update(AddressUpdateDTO dto)
    {
        return _mapper.Map<AddressDTO>(await _repository.Update(_mapper.Map<AddressModel>(dto)));
    }
}