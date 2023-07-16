using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class SolutionVM
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; } = null!;

        [StringLength(1500)]
        public string Comment { get; set; }

        [Required]
        public string URL { get; set; } = null!;
        
        public DateTime? UploadDate { get; set; }

        public int TaskItemId { get; set; }

        public string ExecutorId { get; set; } = null!;

        public virtual User Executor { get; set; } = null!;

        public virtual TaskItem TaskItem { get; set; } = null!;
    }
}
