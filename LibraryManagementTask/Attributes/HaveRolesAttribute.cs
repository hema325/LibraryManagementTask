using LibraryManagementTask.Enums;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementTask.Attributes
{
    public class HaveRolesAttribute: AuthorizeAttribute
    {
        public HaveRolesAttribute(params Roles[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
