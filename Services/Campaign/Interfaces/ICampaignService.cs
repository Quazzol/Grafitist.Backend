using Grafitist.Contracts.Campaign.Request;
using Grafitist.Contracts.Campaign.Response;
using Grafitist.Misc;
using Grafitist.Misc.Enums;

namespace Grafitist.Services.Campaign.Interfaces;

public interface ICampaignService
{
    public Task<IEnumerable<CampaignDTO>> Get(Pager pager);
    public Task<CampaignDTO?> Get(Guid id);
    public Task<IEnumerable<CampaignDTO>> Get(CampaignType type);
    public Task<CampaignDTO> Insert(CampaignInsertDTO dto);
    public Task<CampaignDTO> Update(CampaignUpdateDTO dto);
    public Task Delete(Guid id);
    public Task<CampaignDTO> Deactivate(Guid id);
}