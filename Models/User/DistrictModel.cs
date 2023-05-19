using System.ComponentModel.DataAnnotations.Schema;

namespace Grafitist.Models.User;

[Table("District")]
public class DistrictModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int CityId { get; set; }
}