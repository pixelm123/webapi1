using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Extensions
{
    public static class ClaimsExtensions //Claims provide more flexibility by storing additional user attributes, which can represent permissions, roles, or custom data - preferable over roles
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value; //retrieves the username (or given name) from the user's claims - from token service
        }
    }
}