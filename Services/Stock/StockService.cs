using AutoMapper;
using Grafitist.Contracts.Stock.Request;
using Grafitist.Contracts.Stock.Response;
using Grafitist.Misc;
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

    public async Task<IEnumerable<StockDTO>> Get(IEnumerable<int> productIds)
    {
        return _mapper.Map<IEnumerable<StockDTO>>(await _repository.Get(productIds));
    }

    public async Task<StockDTO> Reserve(StockQuantityDTO dto)
    {
        var stock = await _repository.Get(dto.ProductId);
        if (stock is null)
        {
            return _mapper.Map<StockDTO>(await _repository.Insert(new StockModel { ProductId = dto.ProductId }));
        }
        stock.OrderQty += dto.Quantity.ZeroIfNegative();
        return _mapper.Map<StockDTO>(await _repository.Update(stock));
    }

    public async Task<IEnumerable<StockDTO>> Reserve(IEnumerable<StockQuantityDTO> dtos)
    {
        var stocks = await _repository.Get(dtos.Select(q => q.ProductId));
        foreach (var stock in stocks)
        {
            stock.OrderQty = (stock.OrderQty + dtos.First(q => q.ProductId == stock.ProductId).Quantity).ZeroIfNegative();
        }
        return _mapper.Map<IEnumerable<StockDTO>>(await _repository.Update(stocks));
    }

    public async Task<StockDTO> Unreserve(StockQuantityDTO dto)
    {
        var stock = await _repository.Get(dto.ProductId);
        if (stock is null)
        {
            return _mapper.Map<StockDTO>(await _repository.Insert(new StockModel { ProductId = dto.ProductId }));
        }

        stock.OrderQty = (stock.OrderQty - dto.Quantity.ZeroIfNegative()).ZeroIfNegative();
        return _mapper.Map<StockDTO>(await _repository.Update(stock));
    }

    public async Task<IEnumerable<StockDTO>> Unreserve(IEnumerable<StockQuantityDTO> dtos)
    {
        var stocks = await _repository.Get(dtos.Select(q => q.ProductId));
        foreach (var stock in stocks)
        {
            stock.OrderQty = (stock.OrderQty - dtos.First(q => q.ProductId == stock.ProductId).Quantity).ZeroIfNegative();
        }
        return _mapper.Map<IEnumerable<StockDTO>>(await _repository.Update(stocks));
    }

    public async Task<StockDTO> AddStock(StockQuantityDTO dto)
    {
        var stock = await _repository.Get(dto.ProductId);
        if (stock is null)
        {
            return _mapper.Map<StockDTO>(await _repository.Insert(new StockModel { ProductId = dto.ProductId, Quantity = dto.Quantity }));
        }
        stock.Quantity += dto.Quantity.ZeroIfNegative();
        return _mapper.Map<StockDTO>(await _repository.Update(stock));
    }
}