using AutoMapper;
using Grafitist.Contracts.Cart.Request;
using Grafitist.Contracts.Cart.Response;
using Grafitist.Models.Cart;

namespace Grafitist.Profiles;

public class CartProfile : Profile
{
    public CartProfile()
    {
        // Source -> Target
        CreateMap<CartModel, CartDTO>();
        CreateMap<CartLineModel, CartLineDTO>();
        CreateMap<CartLineInsertDTO, CartLineModel>();
        CreateMap<CartLineUpdateDTO, CartLineModel>();
    }
}