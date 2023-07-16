using Domain.Entites;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class SolutionVM
    {
        [Key]
        public int AssId { get; set; }

        [Required]
        public string Status { get; set; } = null!;

        [StringLength(500, MinimumLength = 0)]
        public string Comment { get; set; } = null!;

        [Required]
        public string URL { get; set; } = null!;

        [Required]
        public DateTime UploadDate { get; set; }

        public int TaskItemId { get; set; }

        public string ExecutorId { get; set; } = null!;

        public virtual User Executor { get; set; } = null!;

        public virtual TaskItem TaskItem { get; set; } = null!;
    }
}
