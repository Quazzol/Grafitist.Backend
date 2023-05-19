using Grafitist.Misc;
using Grafitist.Misc.Enums;
using Grafitist.Models.Campaign;

namespace Grafitist.Repositories.Campaign.Interfaces;

public interface ICampaignRepository
{
    public Task Delete(Guid id);
    public Task<IEnumerable<CampaignModel>> Get(Pager pager);
    public Task<CampaignModel?> Get(Guid id);
    public Task<IEnumerable<CampaignModel>> Get(CampaignType type);
    public Task<CampaignModel> Insert(CampaignModel model);
    public Task<CampaignModel> Update(CampaignModel model);
}