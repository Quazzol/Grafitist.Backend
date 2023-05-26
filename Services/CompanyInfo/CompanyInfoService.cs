using AutoMapper;
using Grafitist.Contracts.CompanyInfo.Request;
using Grafitist.Contracts.CompanyInfo.Response;
using Grafitist.Models.CompanyInfo;
using Grafitist.Repositories.CompanyInfo.Interfaces;
using Grafitist.Services.CompanyInfo.Interfaces;

namespace Grafitist.Services.CompanyInfo;

public class CompanyInfoService : ICompanyInfoService
{
    private readonly ICompanyInfoRepository _repository;
    private readonly IMapper _mapper;

    public CompanyInfoService(ICompanyInfoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<CompanyInfoDTO> Get()
    {
        return _mapper.Map<CompanyInfoDTO>(await _repository.Get());
    }

    public async Task<CompanyInfoDTO> Update(CompanyInfoUpdateDTO dto)
    {
        return _mapper.Map<CompanyInfoDTO>(await _repository.Update(_mapper.Map<CompanyInfoModel>(dto)));
    }
}