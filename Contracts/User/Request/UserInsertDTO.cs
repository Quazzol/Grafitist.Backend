using System.ComponentModel.DataAnnotations;
using Grafitist.Misc.Enums;

namespace Grafitist.Contracts.User.Request;

public class UserInsertDTO
{
    [Required] public UserType Type { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    [Required] public string? Email { get; set; }
    [Required] public string? Password { get; set; }
    public string? Phone { get; set; }
}