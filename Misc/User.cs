using Grafitist.Misc.Enums;

namespace Grafitist.Misc;

public class User
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public UserType Type { get; set; }
}