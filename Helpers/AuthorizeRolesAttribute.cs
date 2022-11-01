using Microsoft.AspNetCore.Authorization;

namespace HolaMundoJwt.Helpers
{
    public static class Rol
    {
        public const int User = 1;
        public const int Admin = 2;
    }
    
    
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params int[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}