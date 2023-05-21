using AutoMapper;
using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Grafitist.Services.Product.Interfaces;

namespace Grafitist.Services.Product;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
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

    public async Task<CategoryDTO?> Get(int id)
    {
        return _mapper.Map<CategoryDTO>(await _repository.Get(id));
    }

    public async Task<IEnumerable<CategoryDTO>> Get()
    {
        return _mapper.Map<IEnumerable<CategoryDTO>>(await _repository.Get());
    }

    public async Task<CategoryDTO> Insert(CategoryInsertDTO dto)
    {
        return _mapper.Map<CategoryDTO>(await _repository.Insert(_mapper.Map<CategoryModel>(dto)));
    }

    public async Task<CategoryDTO> Update(CategoryUpdateDTO dto)
    {
        return _mapper.Map<CategoryDTO>(await _repository.Update(_mapper.Map<CategoryModel>(dto)));
    }
}