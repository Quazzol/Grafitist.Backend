using Grafitist.Connection;
using Grafitist.Models.CompanyInfo;
using Grafitist.Repositories.CompanyInfo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grafitist.Repositories.CompanyInfo;

public class CompanyInfoRepository : ICompanyInfoRepository
{
    private readonly AppDbContext _context;

    public CompanyInfoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CompanyInfoModel> Get()
    {
        return await _context.CompanyInfos!.SingleAsync();
    }

    public async Task<CompanyInfoModel> Update(CompanyInfoModel model)
    {
        if (model is null) throw new ArgumentNullException(nameof(CompanyInfoModel));
        var companyInfo = await _context.CompanyInfos!.SingleAsync();
        if (companyInfo is null) throw new Exception($"Model.Id not found! {model.Id}");

        companyInfo.AboutUs = model.AboutUs;
        companyInfo.Address = model.Address;
        companyInfo.Phone = model.Phone;
        companyInfo.Email = model.Email;
        companyInfo.GoogleMapLink = model.GoogleMapLink;
        await _context.SaveChangesAsync();
        return companyInfo;
    }
}