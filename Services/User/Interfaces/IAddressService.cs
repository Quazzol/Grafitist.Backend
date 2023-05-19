using Grafitist.Contracts.User.Request;
using Grafitist.Contracts.User.Response;

namespace Grafitist.Services.User.Interfaces;

public interface IAddressService
{
    public Task Delete(int id);
    public Task<IEnumerable<AddressDTO?>> Get(Guid userId);
    public Task<AddressDTO?> Get(int id);
    public Task<AddressDTO> Insert(AddressInsertDTO dto);
    public Task<AddressDTO> Update(AddressUpdateDTO dto);
}