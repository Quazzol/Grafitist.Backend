using AutoMapper;
using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Grafitist.Services.Product.Interfaces;

namespace Grafitist.Services.Product;

public class VariantService : IVariantService
{
    private readonly IVariantRepository _repository;
    private readonly IMapper _mapper;

    public VariantService(IVariantRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Deactivate(int id)
    {
        var model = await _repository.Get(id);
        if (model is null)
            return;
        model.IsActive = false;
        await _repository.Update(model);
    }

    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }

    public async Task<VariantDTO?> Get(int id)
    {
        return _mapper.Map<VariantDTO>(await _repository.Get(id));
    }

    public async Task<IEnumerable<VariantDTO>> Get()
    {
        return _mapper.Map<IEnumerable<VariantDTO>>(await _repository.Get());
    }

    public async Task<VariantDTO> Insert(VariantInsertDTO dto)
    {
        return _mapper.Map<VariantDTO>(await _repository.Insert(_mapper.Map<VariantModel>(dto)));
    }

    public async Task<VariantDTO> Update(VariantUpdateDTO dto)
    {
        return _mapper.Map<VariantDTO>(await _repository.Update(_mapper.Map<VariantModel>(dto)));
    }
}