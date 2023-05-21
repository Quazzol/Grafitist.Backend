using AutoMapper;
using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Grafitist.Services.Product.Interfaces;

namespace Grafitist.Services.Product;

public class ItemService : IItemService
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;

    public ItemService(IItemRepository repository, IMapper mapper)
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

    public async Task<ItemDTO?> Get(int id)
    {
        return _mapper.Map<ItemDTO>(await _repository.Get(id));
    }

    public async Task<IEnumerable<ItemDTO>> Get()
    {
        return _mapper.Map<IEnumerable<ItemDTO>>(await _repository.Get());
    }

    public async Task<ItemDTO> Insert(ItemInsertDTO dto)
    {
        return _mapper.Map<ItemDTO>(await _repository.Insert(_mapper.Map<ItemModel>(dto)));
    }

    public async Task<ItemDTO> Update(ItemUpdateDTO dto)
    {
        return _mapper.Map<ItemDTO>(await _repository.Update(_mapper.Map<ItemModel>(dto)));
    }
}