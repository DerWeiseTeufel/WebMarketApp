using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ShortDescr { get; set; } = null!;

        public DateTime Deadline { get; set; }

        public double Reward { get; set; }
        
        public string CreatorId { get; set; } = null!;

        public virtual User Creator { get; set; } = null!;

        public virtual ICollection<Solution> Solutions { get; set; } = new List<Solution>();
    }
}
