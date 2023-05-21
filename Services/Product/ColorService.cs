using AutoMapper;
using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Grafitist.Services.Product.Interfaces;

namespace Grafitist.Services.Product;

public class ColorService : IColorService
{
    private readonly IColorRepository _repository;
    private readonly IMapper _mapper;

    public ColorService(IColorRepository repository, IMapper mapper)
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

    public async Task<ColorDTO?> Get(int id)
    {
        return _mapper.Map<ColorDTO>(await _repository.Get(id));
    }

    public async Task<IEnumerable<ColorDTO>> Get()
    {
        return _mapper.Map<IEnumerable<ColorDTO>>(await _repository.Get());
    }

    public async Task<ColorDTO> Insert(ColorInsertDTO dto)
    {
        return _mapper.Map<ColorDTO>(await _repository.Insert(_mapper.Map<ColorModel>(dto)));
    }

    public async Task<ColorDTO> Update(ColorUpdateDTO dto)
    {
        return _mapper.Map<ColorDTO>(await _repository.Update(_mapper.Map<ColorModel>(dto)));
    }
}