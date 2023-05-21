using AutoMapper;
using Grafitist.Contracts.Campaign.Request;
using Grafitist.Contracts.Campaign.Response;
using Grafitist.Misc;
using Grafitist.Misc.Enums;
using Grafitist.Models.Campaign;
using Grafitist.Repositories.Campaign.Interfaces;
using Grafitist.Services.Campaign.Interfaces;

namespace Grafitist.Services.Campaign;

public class CampaignService : ICampaignService
{
    private readonly ICampaignRepository _repository;
    private readonly IMapper _mapper;

    public CampaignService(ICampaignRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Deactivate(Guid id)
    {
        var model = await _repository.Get(id);
        if (model is null)
            return;
        model.IsActive = false;
        await _repository.Update(model);
    }

    public async Task Delete(Guid id)
    {
        await _repository.Delete(id);
    }

    public async Task<IEnumerable<CampaignDTO>> Get(Pager pager)
    {
        return _mapper.Map<IEnumerable<CampaignDTO>>(await _repository.Get(pager));
    }

    public async Task<CampaignDTO?> Get(Guid id)
    {
        return _mapper.Map<CampaignDTO>(await _repository.Get(id));
    }

    public async Task<IEnumerable<CampaignDTO>> Get(CampaignType type)
    {
        return _mapper.Map<IEnumerable<CampaignDTO>>(await _repository.Get(type));
    }

    public async Task<CampaignDTO> Insert(CampaignInsertDTO dto)
    {
        return _mapper.Map<CampaignDTO>(await _repository.Insert(_mapper.Map<CampaignModel>(dto)));
    }

    public async Task<CampaignDTO> Update(CampaignUpdateDTO dto)
    {
        return _mapper.Map<CampaignDTO>(await _repository.Update(_mapper.Map<CampaignModel>(dto)));
    }
}