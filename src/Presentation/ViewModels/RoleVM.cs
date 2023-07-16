using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class RoleVM
    {
        public string? RoleName { get; set; }

        public string? NewRoleName { get; set; }

        [EmailAddress]
        public string? UserEmail { get; set; }
    }
}
