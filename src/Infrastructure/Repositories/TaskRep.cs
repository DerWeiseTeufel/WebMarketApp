using Application.DomainServices;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Reflection.Metadata.Ecma335;

namespace Infrastructure.Repositories
{
    public class TaskRep : EntityBaseRep<TaskItem>, ITaskItemRep
    {
        private readonly AppDbContext dbContext;

        public TaskRep(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TaskItem?> GetByIdAsync(int id) =>
            await dbContext.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
    }
}
