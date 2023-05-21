using AutoMapper;
using Grafitist.Contracts.Order.Request;
using Grafitist.Contracts.Order.Response;
using Grafitist.Models.Order;

namespace Grafitist.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        // Source -> Target
        CreateMap<OrderModel, OrderDTO>();
        CreateMap<OrderInsertDTO, OrderModel>();
        CreateMap<OrderUpdateDTO, OrderModel>();
    }
}