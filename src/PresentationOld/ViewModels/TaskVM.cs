using Domain.Entites;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class TaskVM
    {
        public int TaskItemId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 15, ErrorMessage = "The field must be at least {2} and at max {1} characters long.")]
        public string TaskItemName { get; set; } = null!;

        [Required]
        [StringLength(1500, MinimumLength = 50, ErrorMessage = "The field must be at least {2} and at max {1} characters long.")]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(200, MinimumLength = 15, ErrorMessage = "The field must be at least {2} and at max {1} characters long.")]
        public string ShortDescr { get; set; } = null!;

        [Required]
        public DateTime Deadline { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The rawerd must be in the range of {2} and {1}.")]
        public double Reward { get; set; }

        public string? CreatorId { get; set; }

        public virtual User? Creator { get; set; }

        public virtual ICollection<Solution>? Solutions { get; set; } = new List<Solution>();
    }
}
