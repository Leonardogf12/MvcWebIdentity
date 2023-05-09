using Microsoft.AspNetCore.Identity;

namespace MvcWebIdentity.Areas.Admin.Models
{
    public class RoleEdit
    {
        public IdentityRole? Role { get; set; }
        public IEnumerable<IdentityUser>? Membros { get; set;}
        public IEnumerable<IdentityUser>? SemMembros { get; set;}
    }
}
