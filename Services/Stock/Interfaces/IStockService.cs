using Grafitist.Contracts.Stock.Request;
using Grafitist.Contracts.Stock.Response;

namespace Grafitist.Services.Stock.Interfaces;

public interface IStockService
{
    public Task<StockDTO?> Get(int productId);
    public Task<IEnumerable<StockDTO>> Get(int[] productIds);
    public Task<StockDTO> Save(StockUpdateDTO dto);
}