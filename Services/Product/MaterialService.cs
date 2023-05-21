using AutoMapper;
using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Grafitist.Services.Product.Interfaces;

namespace Grafitist.Services.Product;

public class MaterialService : IMaterialService
{
    private readonly IMaterialRepository _repository;
    private readonly IMapper _mapper;

    public MaterialService(IMaterialRepository repository, IMapper mapper)
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

    public async Task<MaterialDTO?> Get(int id)
    {
        return _mapper.Map<MaterialDTO>(await _repository.Get(id));
    }

    public async Task<IEnumerable<MaterialDTO>> Get()
    {
        return _mapper.Map<IEnumerable<MaterialDTO>>(await _repository.Get());
    }

    public async Task<MaterialDTO> Insert(MaterialInsertDTO dto)
    {
        return _mapper.Map<MaterialDTO>(await _repository.Insert(_mapper.Map<MaterialModel>(dto)));
    }

    public async Task<MaterialDTO> Update(MaterialUpdateDTO dto)
    {
        return _mapper.Map<MaterialDTO>(await _repository.Update(_mapper.Map<MaterialModel>(dto)));
    }
}