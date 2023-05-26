using Grafitist.Contracts.CompanyInfo.Request;
using Grafitist.Contracts.CompanyInfo.Response;

namespace Grafitist.Services.CompanyInfo.Interfaces;

public interface ICompanyInfoService
{
    public Task<CompanyInfoDTO> Get();
    public Task<CompanyInfoDTO> Update(CompanyInfoUpdateDTO dto);
}