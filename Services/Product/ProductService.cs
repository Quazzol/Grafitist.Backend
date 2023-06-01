using AutoMapper;
using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;
using Grafitist.Misc;
using Grafitist.Misc.Interfaces;
using Grafitist.Models.Product;
using Grafitist.Repositories.Product.Interfaces;
using Grafitist.Services.Product.Interfaces;

namespace Grafitist.Services.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPriceManager _priceManager;

    public ProductService(IProductRepository repository, IMapper mapper, IPriceManager priceManager)
    {
        _repository = repository;
        _mapper = mapper;
        _priceManager = priceManager;
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

    public async Task<ProductDTO?> Get(int id)
    {
        var product = _mapper.Map<ProductDTO>(await _repository.Get(id));
        await _priceManager.SetDiscountedPrice(product);
        return product;
    }

    public async Task<IEnumerable<ProductDTO>> Get(Pager? pager, ProductFilter? filter)
    {
        var products = _mapper.Map<IEnumerable<ProductDTO>>(await _repository.Get(pager, filter));
        await _priceManager.SetDiscountedPrice(products);
        return products;
    }

    public async Task<int> Get(ProductFilter? filter)
    {
        return await _repository.Get(filter);
    }

    public async Task<ProductDTO> Insert(ProductInsertDTO dto)
    {
        return _mapper.Map<ProductDTO>(await _repository.Insert(_mapper.Map<ProductModel>(dto)));
    }

    public async Task<ProductDTO> Update(ProductUpdateDTO dto)
    {
        return _mapper.Map<ProductDTO>(await _repository.Update(_mapper.Map<ProductModel>(dto)));
    }
}