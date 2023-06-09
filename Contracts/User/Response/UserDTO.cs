using Grafitist.Misc.Enums;

namespace Grafitist.Contracts.User.Response;

public class UserDTO
{
    public Guid Id { get; set; }
    public UserType Type { get; set; } = UserType.Anonymous;
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public IEnumerable<AddressDTO>? Addresses { get; set; }
}