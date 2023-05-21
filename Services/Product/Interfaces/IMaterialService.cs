using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;

namespace Grafitist.Services.Product.Interfaces;

public interface IMaterialService
{
    public Task<MaterialDTO?> Get(int id);
    public Task<IEnumerable<MaterialDTO>> Get();
    public Task<MaterialDTO> Insert(MaterialInsertDTO dto);
    public Task<MaterialDTO> Update(MaterialUpdateDTO dto);
    public Task Delete(int id);
    public Task Deactivate(int id);
}