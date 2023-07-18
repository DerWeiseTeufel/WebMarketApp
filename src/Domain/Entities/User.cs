using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User : IdentityUser, IUndeletable
    {
        public string Name { get; set; } = null!;

        public int Reputation { get; set; }

        public double VirtualBalance { get; set; }

        public string? ProfilePicture { get; set; }

        public bool IsRemoved { get; set; }

        public virtual ICollection<Solution> Solutions { get; set; } = new List<Solution>();

        public virtual ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();

        [NotMapped]
        public IEnumerable<TaskItem> AvailableTasks
        {
            get => TaskItems.Where(s => !s.IsRemoved).ToList();
            set => TaskItems = value.ToList();
        }

        [NotMapped]
        public IEnumerable<Solution> AvailableSolutions
        {
            get => Solutions.Where(s => !s.IsRemoved).ToList();
            set => Solutions = value.ToList();
        }
    }
}
