using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.User.Request;

public class CityInsertDTO
{
    [Required] public string? Name { get; set; }
}