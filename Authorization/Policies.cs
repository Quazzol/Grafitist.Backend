namespace Grafitist.Authorization;

public static class Policies
{
    public const string Admin = nameof(Admin);
    public const string User = nameof(User);
    public const string HasLoggedIn = nameof(HasLoggedIn);
}