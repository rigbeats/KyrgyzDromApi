using KDrom.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace KDrom.WebApi.Attribute;

public class AuthorizeRoleAttribute : AuthorizeAttribute
{
    public AuthorizeRoleAttribute(params UserRole[] roles)
    {
        Roles = string.Join(",", roles.Select(x => x.ToString()));
    }
}
