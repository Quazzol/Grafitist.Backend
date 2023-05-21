using Grafitist.Contracts.Product.Request;
using Grafitist.Contracts.Product.Response;

namespace Grafitist.Services.Product.Interfaces;

public interface IVariantService
{
    public Task<VariantDTO?> Get(int id);
    public Task<IEnumerable<VariantDTO>> Get();
    public Task<VariantDTO> Insert(VariantInsertDTO dto);
    public Task<VariantDTO> Update(VariantUpdateDTO dto);
    public Task Delete(int id);
    public Task Deactivate(int id);
}