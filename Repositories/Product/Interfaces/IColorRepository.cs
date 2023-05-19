using Grafitist.Models.Product;

namespace Grafitist.Repositories.Product.Interfaces;

public interface IColorRepository
{
    public Task<ColorModel?> Get(int id);
    public Task<IEnumerable<ColorModel>> Get();
    public Task<ColorModel> Insert(ColorModel model);
    public Task<ColorModel> Update(ColorModel model);
    public Task Delete(int id);
}