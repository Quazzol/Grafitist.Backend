using Microsoft.AspNetCore.Authorization;

namespace Grafitist.Authorization;

public class AdminRequirement : IAuthorizationRequirement
{ }

public class UserRequirement : IAuthorizationRequirement
{ }

public class HasLoggedInRequirement : IAuthorizationRequirement
{ }