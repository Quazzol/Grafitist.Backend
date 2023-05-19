using System.ComponentModel.DataAnnotations.Schema;
using Grafitist.Misc.Enums;

namespace Grafitist.Models.User;

[Table("User")]
public class UserModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public UserType Type { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Phone { get; set; }
    public ICollection<AddressModel>? Addresses { get; set; }
}