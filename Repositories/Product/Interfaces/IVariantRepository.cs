using Grafitist.Models.Product;

namespace Grafitist.Repositories.Product.Interfaces;

public interface IVariantRepository
{
    public Task<VariantModel?> Get(int id);
    public Task<IEnumerable<VariantModel>> Get();
    public Task<VariantModel> Insert(VariantModel model);
    public Task<VariantModel> Update(VariantModel model);
    public Task Delete(int id);
}