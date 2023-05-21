using AutoMapper;
using Grafitist.Contracts.Stock.Request;
using Grafitist.Contracts.Stock.Response;
using Grafitist.Models.Stock;
using Grafitist.Repositories.Stock.Interfaces;
using Grafitist.Services.Stock.Interfaces;

namespace Grafitist.Services.Stock;

public class StockService : IStockService
{
    private readonly IStockRepository _repository;
    private readonly IMapper _mapper;

    public StockService(IStockRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<StockDTO?> Get(int productId)
    {
        return _mapper.Map<StockDTO>(await _repository.Get(productId));
    }

    public async Task<IEnumerable<StockDTO>> Get(int[] productIds)
    {
        return _mapper.Map<IEnumerable<StockDTO>>(await _repository.Get(productIds));
    }

    public async Task<StockDTO> Save(StockUpdateDTO dto)
    {
        var model = new StockModel();
        try
        {
            model = await _repository.Update(_mapper.Map<StockModel>(dto));
        }
        catch (KeyNotFoundException)
        {
            model = await _repository.Insert(_mapper.Map<StockModel>(dto));
        }

        return _mapper.Map<StockDTO>(model);
    }
}