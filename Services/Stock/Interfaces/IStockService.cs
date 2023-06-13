using Grafitist.Contracts.Stock.Request;
using Grafitist.Contracts.Stock.Response;

namespace Grafitist.Services.Stock.Interfaces;

public interface IStockService
{
    public Task<StockDTO?> Get(int productId);
    public Task<IEnumerable<StockDTO>> Get(IEnumerable<int> productIds);
    public Task<StockDTO> Reserve(StockQuantityDTO dto);
    public Task<IEnumerable<StockDTO>> Reserve(IEnumerable<StockQuantityDTO> dtos);
    public Task<StockDTO> Unreserve(StockQuantityDTO dto);
    public Task<IEnumerable<StockDTO>> Unreserve(IEnumerable<StockQuantityDTO> dtos);
    public Task<StockDTO> AddStock(StockQuantityDTO dto);
}