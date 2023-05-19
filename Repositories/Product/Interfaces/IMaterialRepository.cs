using Grafitist.Models.Product;

namespace Grafitist.Repositories.Product.Interfaces;

public interface IMaterialRepository
{
    public Task<MaterialModel?> Get(int id);
    public Task<IEnumerable<MaterialModel>> Get();
    public Task<MaterialModel> Insert(MaterialModel model);
    public Task<MaterialModel> Update(MaterialModel model);
    public Task Delete(int id);
}