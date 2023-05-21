using AutoMapper;
using Grafitist.Contracts.User.Request;
using Grafitist.Contracts.User.Response;
using Grafitist.Models.User;

namespace Grafitist.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Source -> Target
        CreateMap<UserModel, UserDTO>();
        CreateMap<UserInsertDTO, UserModel>();
        CreateMap<UserUpdateDTO, UserModel>();
        CreateMap<UserSignInDTO, UserModel>();
        CreateMap<AddressModel, AddressDTO>();
        CreateMap<AddressInsertDTO, AddressModel>();
        CreateMap<AddressUpdateDTO, AddressModel>();
        CreateMap<CityModel, CityDTO>();
        CreateMap<CityInsertDTO, CityModel>();
        CreateMap<DistrictModel, DistrictDTO>();
        CreateMap<DistrictInsertDTO, DistrictModel>();
    }
}