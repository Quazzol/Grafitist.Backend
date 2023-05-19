using Grafitist.Models.Stock;

namespace Grafitist.Repositories.Stock.Interfaces;

public interface IStockRepository
{
    public Task Delete(Guid id);
    public Task<StockModel?> Get(int productId);
    public Task<IEnumerable<StockModel>> Get(int[] productIds);
    public Task<StockModel> Insert(StockModel model);
    public Task<StockModel> Update(StockModel model);
}