namespace Grafitist.Misc.Interfaces;

public interface IUserContext
{
    public User? CurrentUser { get; }
}