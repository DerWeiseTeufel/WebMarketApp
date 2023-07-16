using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }

        DbSet<TaskItem> TaskItems { get; }

        DbSet<Solution> Solutions { get; }
    }
}
