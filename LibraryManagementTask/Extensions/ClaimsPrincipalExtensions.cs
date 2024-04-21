using System.Security.Claims;

namespace LibraryManagementTask.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? Id(this ClaimsPrincipal claimsPrincipal)
        {
            var id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
                return null;

            return int.Parse(id);
        }
    }
}
