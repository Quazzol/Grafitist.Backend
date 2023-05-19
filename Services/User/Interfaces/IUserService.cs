using Grafitist.Contracts.User.Request;
using Grafitist.Contracts.User.Response;
using Grafitist.Misc;

namespace Grafitist.Services.User.Interfaces;

public interface IUserService
{
    public Task Delete(Guid id);
    public Task<UserDTO?> Get(Guid id);
    public Task<IEnumerable<UserDTO>> Get(Pager pager);
    public Task<UserDTO> Insert(UserInsertDTO dto);
    public Task<UserDTO> Update(UserUpdateDTO dto);
    public Task<string> SignIn(UserSignInDTO dto);
}