using Azure;
using Azure.Core;
using IS_Task.Shared.Constants;
using System.Security.Claims;

namespace IS_Task.Shared.Helpers
{
    public static class AuthenticationHelper
    {
        public static long? GetUserId(ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return null;
            return long.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public static string? GetCartToken(ClaimsPrincipal user, HttpRequest request, HttpResponse response)
        {
            if (user.Identity.IsAuthenticated)
                return null;

            var token = request.Cookies[DataConstants.CartSessionKey];
            if (token == null)
            {
                token = Guid.NewGuid().ToString();
                response.Cookies.Append(DataConstants.CartSessionKey, token, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(2)
                });
            }
            return token;
        }
    }
}
