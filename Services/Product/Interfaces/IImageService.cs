using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;

namespace Grafitist.Services.Product.Interfaces;

public interface IImageService
{
    public Task<ImageDTO?> Get(int id);
    public Task<IEnumerable<ImageDTO?>> GetByProduct(int productId);
    public Task<ImageDTO?> Insert(ImageInsertDTO image);
    public Task<ImageDTO?> Update(ImageUpdateDTO image);
    public Task Delete(int id);
}