using Grafitist.Models.User;

namespace Grafitist.Repositories.User.Interfaces;

public interface IAddressRepository
{
    public Task Delete(int id);
    public Task<IEnumerable<AddressModel?>> Get(Guid userId);
    public Task<AddressModel?> Get(int id);
    public Task<AddressModel> Insert(AddressModel model);
    public Task<AddressModel> Update(AddressModel model);
}