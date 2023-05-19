using Grafitist.Misc;
using Grafitist.Models.User;

namespace Grafitist.Repositories.User.Interfaces;

public interface IUserRepository
{
    public Task Delete(Guid id);
    public Task<UserModel?> Get(Guid id);
    public Task<IEnumerable<UserModel>> Get(Pager pager);
    public Task<UserModel> Insert(UserModel model);
    public Task<UserModel> Update(UserModel model);
    public Task<UserModel?> SignIn(UserModel model);
}