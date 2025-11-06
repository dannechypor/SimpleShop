using System.Security.Claims;

namespace SimpleShop.Web.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetFirstName(this ClaimsPrincipal principal)
    {
        var firstName = principal.Claims.FirstOrDefault(c => c.Type == "FirstName");
        return firstName?.Value;
    }

    public static string GetLastName(this ClaimsPrincipal principal)
    {
        var lastName = principal.Claims.FirstOrDefault(c => c.Type == "LastName");
        return lastName?.Value;
    }
}