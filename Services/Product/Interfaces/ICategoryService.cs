using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;

namespace Grafitist.Services.Product.Interfaces;

public interface ICategoryService
{
    public Task Deactivate(int id);
    public Task Delete(int id);
    public Task<CategoryDTO?> Get(int id);
    public Task<IEnumerable<CategoryDTO>> Get();
    public Task<CategoryDTO> Insert(CategoryInsertDTO dto);
    public Task<CategoryDTO> Update(CategoryUpdateDTO dto);
}