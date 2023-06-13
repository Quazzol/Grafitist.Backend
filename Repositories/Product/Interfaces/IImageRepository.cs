using Grafitist.Models.Product;

namespace Grafitist.Repositories.Product.Interfaces;

public interface IImageRepository
{
    public Task<ImageModel?> Get(int id);
    public Task<IEnumerable<ImageModel>> GetByProduct(int productId);
    public Task<ImageModel> Insert(ImageModel model);
    public Task<ImageModel> Update(ImageModel model);
    public Task Delete(int id);
}