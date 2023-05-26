using Grafitist.Models.CompanyInfo;

namespace Grafitist.Repositories.CompanyInfo.Interfaces;

public interface ICompanyInfoRepository
{
    public Task<CompanyInfoModel> Get();
    public Task<CompanyInfoModel> Update(CompanyInfoModel model);
}