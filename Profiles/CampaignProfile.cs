using AutoMapper;
using Grafitist.Contracts.Campaign.Request;
using Grafitist.Contracts.Campaign.Response;
using Grafitist.Models.Campaign;

namespace Grafitist.Profiles;

public class CampaignProfile : Profile
{
    public CampaignProfile()
    {
        // Source -> Target
        CreateMap<CampaignModel, CampaignDTO>();
        CreateMap<CampaignInsertDTO, CampaignModel>();
        CreateMap<CampaignUpdateDTO, CampaignModel>();
    }
}