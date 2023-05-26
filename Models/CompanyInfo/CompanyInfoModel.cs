using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grafitist.Models.CompanyInfo;

[Table("CompanyInfo")]
public class CompanyInfoModel
{
    [Key] public int Id { get; set; }
    public string? AboutUs { get; set; }
    public string? Address { get; set; }
    public string? GoogleMapLink { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}