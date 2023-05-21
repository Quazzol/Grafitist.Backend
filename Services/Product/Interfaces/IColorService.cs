using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;

namespace Grafitist.Services.Product.Interfaces;

public interface IColorService
{
    public Task<ColorDTO?> Get(int id);
    public Task<IEnumerable<ColorDTO>> Get();
    public Task<ColorDTO> Insert(ColorInsertDTO dto);
    public Task<ColorDTO> Update(ColorUpdateDTO dto);
    public Task Delete(int id);
    public Task Deactivate(int id);
}