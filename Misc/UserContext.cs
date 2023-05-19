using Grafitist.Misc.Interfaces;

namespace Grafitist.Misc;

public class UserContext : IUserContext
{
    public User? CurrentUser { get; set; }
}