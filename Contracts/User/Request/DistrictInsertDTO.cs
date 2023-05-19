using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.User.Request;

public class DistrictInsertDTO
{
    [Required] public string? Name { get; set; }
    [Required] public int CityId { get; set; }
}