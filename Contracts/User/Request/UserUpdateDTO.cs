using System.ComponentModel.DataAnnotations;
using Grafitist.Misc.Enums;

namespace Grafitist.Contracts.User.Request;

public class UserUpdateDTO
{
    [Required] public Guid Id { get; set; }
    public UserType Type { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Phone { get; set; }
}