using AutoMapper;
using Grafitist.Contracts.Stock.Request;
using Grafitist.Contracts.Stock.Response;
using Grafitist.Models.Stock;

namespace Grafitist.Profiles;

public class StockProfile : Profile
{
    public StockProfile()
    {
        // Source -> Target
        CreateMap<StockModel, StockDTO>();
        CreateMap<StockQuantityDTO, StockModel>();
    }
}