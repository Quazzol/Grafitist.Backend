using Grafitist.Connection;
using Grafitist.Misc;
using Grafitist.Misc.Enums;
using Grafitist.Models.Campaign;
using Grafitist.Repositories.Campaign.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.Campaign;

public class CampaignRepository : ICampaignRepository
{
    private readonly AppDbContext _context;

    public CampaignRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Delete(Guid id)
    {
        var campaign = await _context.Campaigns!.FirstOrDefaultAsync(q => q.Id == id);
        if (campaign is null)
            return;

        _context.Campaigns!.Remove(campaign);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CampaignModel>> Get(Pager pager)
    {
        return await _context.Campaigns!.Where(q => !pager.OnlyActive || q.IsActive).OrderBy(q => q.Id).ToListAsync();
    }

    public async Task<CampaignModel?> Get(Guid id)
    {
        return await _context.Campaigns!.FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<IEnumerable<CampaignModel>> Get(CampaignType type)
    {
        return await _context.Campaigns!.Where(q => q.Type == type && q.IsActive).OrderByDescending(q => q.Percent).ToListAsync();
    }

    public async Task<CampaignModel> Insert(CampaignModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(CampaignModel));
        await _context.Campaigns!.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<CampaignModel> Update(CampaignModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(CampaignModel));
        var campaign = await _context.Campaigns!.FirstOrDefaultAsync(q => q.Id == model.Id);
        if (campaign is null) throw new Exception($"Model.Id not found! {model.Id}");

        campaign.Code = model.Code;
        campaign.Description = model.Description;
        campaign.Type = model.Type;
        campaign.CampaignTypeId = model.CampaignTypeId;
        campaign.MinimumPrice = model.MinimumPrice;
        campaign.Percent = model.Percent;
        campaign.IsActive = model.IsActive;
        await _context.SaveChangesAsync();
        return campaign;
    }
}