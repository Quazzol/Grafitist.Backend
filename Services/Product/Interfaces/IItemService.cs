using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;

namespace Grafitist.Services.Product.Interfaces;

public interface IItemService
{
    public Task<ItemDTO?> Get(int id);
    public Task<IEnumerable<ItemDTO>> Get();
    public Task<ItemDTO> Insert(ItemInsertDTO dto);
    public Task<ItemDTO> Update(ItemUpdateDTO dto);
    public Task Delete(int id);
    public Task Deactivate(int id);
}