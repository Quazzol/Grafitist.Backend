using System.ComponentModel.DataAnnotations.Schema;

namespace Grafitist.Models.User;

[Table("Address")]
public class AddressModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int CityId { get; set; }
    [ForeignKey(nameof(CityId))] public CityModel? City { get; set; }
    public int DistrictId { get; set; }
    [ForeignKey(nameof(DistrictId))] public DistrictModel? District { get; set; }
    public string? OpenAddress { get; set; }
    public Guid UserId { get; set; }
}