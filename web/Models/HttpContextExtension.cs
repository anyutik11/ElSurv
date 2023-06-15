using System.Security.Claims;

namespace web.Models;

public static class HttpContextExtension
{
    public static UserData GetUserData(this HttpContext context)
    {
        UserData userData = new UserData();
        var claims = context?.User?.Claims;
        if (claims == null) return userData;

        userData.name = claims.Where(c => c.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
        userData.login = claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
        userData.userId = claims.Where(c => c.Type == "userId").FirstOrDefault()?.Value;
        if (bool.TryParse(claims.Where(c => c.Type == "sysUser").FirstOrDefault()?.Value, out bool sysUser))
        {
            userData.sysUser = sysUser;
        }
        else
        {
            userData.sysUser = false;
        }
        userData.companyId = claims.Where(c => c.Type == "companyId").FirstOrDefault()?.Value;

        return userData;
    }
}
