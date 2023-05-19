using Grafitist.Contracts.User.Response;

namespace Grafitist.Misc.Interfaces;

public interface ITokenProvider
{
    public Task<string> CreateToken(UserDTO? user);
}