using Microsoft.AspNetCore.Identity;

namespace MvcWebIdentity.Areas.Admin.Models
{
    public class RoleModification
    {
        public string? NameRole { get; set; }
        public string? RoleId { get; set; }
        public string[]? AddIds { get; set; }
        public string[]? DeletarIds { get; set; }
    }
}
