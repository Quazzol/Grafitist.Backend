using AutoMapper;
using Grafitist.Contracts.User.Request;
using Grafitist.Contracts.User.Response;
using Grafitist.Misc;
using Grafitist.Misc.Interfaces;
using Grafitist.Models.User;
using Grafitist.Repositories.User.Interfaces;
using Grafitist.Services.User.Interfaces;

namespace Grafitist.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ITokenProvider _tokenProvider;
    private readonly IUserContext _userContext;

    public UserService(IUserRepository repository, IMapper mapper, ITokenProvider tokenProvider, IUserContext userContext)
    {
        _repository = repository;
        _mapper = mapper;
        _tokenProvider = tokenProvider;
        _userContext = userContext;
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }

    public async Task<UserDTO?> Get(Guid id)
    {
        return _mapper.Map<UserDTO>(await _repository.Get(id));
    }

    public async Task<IEnumerable<UserDTO>> Get(Pager pager)
    {
        return _mapper.Map<IEnumerable<UserDTO>>(await _repository.Get(pager));
    }

    public async Task<UserDTO> Insert(UserInsertDTO dto)
    {
        return _mapper.Map<UserDTO>(await _repository.Insert(_mapper.Map<UserModel>(dto)));
    }

    public async Task<string> SignIn(UserSignInDTO dto)
    {
        var model = await _repository.SignIn(_mapper.Map<UserModel>(dto));
        if (model is null)
            return string.Empty;

        return await _tokenProvider.CreateToken(_mapper.Map<UserDTO>(model));
    }

    public async Task<UserDTO> Update(UserUpdateDTO dto)
    {
        return _mapper.Map<UserDTO>(await _repository.Update(_mapper.Map<UserModel>(dto)));
    }
}