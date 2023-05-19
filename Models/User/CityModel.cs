using System.ComponentModel.DataAnnotations.Schema;

namespace Grafitist.Models.User;

[Table("City")]
public class CityModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
}