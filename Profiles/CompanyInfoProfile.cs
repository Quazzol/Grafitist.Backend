using AutoMapper;
using Grafitist.Contracts.CompanyInfo.Request;
using Grafitist.Contracts.CompanyInfo.Response;
using Grafitist.Models.CompanyInfo;

namespace Grafitist.Profiles;

public class CompanyInfoProfile : Profile
{
    public CompanyInfoProfile()
    {
        // Source -> Target
        CreateMap<CompanyInfoModel, CompanyInfoDTO>();
        CreateMap<CompanyInfoUpdateDTO, CompanyInfoModel>();
    }
}