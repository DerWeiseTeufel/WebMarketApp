using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = null!;

        public int Reputation { get; set; }

        public double VirtualBalance { get; set; }

        public string? ProfilePicture { get; set; }

        public virtual ICollection<Solution> Solutions { get; set; } = new List<Solution>();

        public virtual ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
    }
}
