using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.User.Request;

public class AddressUpdateDTO
{
    [Required] public int Id { get; set; }
    [Required] public string? Name { get; set; }
    [Required] public int CityId { get; set; }
    [Required] public int DistrictId { get; set; }
    [Required] public string? OpenAddress { get; set; }
    [Required] public Guid UserId { get; set; }
}