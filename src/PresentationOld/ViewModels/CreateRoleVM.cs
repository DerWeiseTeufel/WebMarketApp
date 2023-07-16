using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class CreateRoleVM
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; } = null!;
    }
}
