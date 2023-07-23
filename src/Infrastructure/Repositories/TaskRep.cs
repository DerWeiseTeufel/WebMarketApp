using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Reflection.Metadata.Ecma335;

namespace Infrastructure.Repositories
{
    public class TaskRep : EntityBaseRep<int, TaskItem>, ITaskItemRep
    {
        private readonly AppDbContext dbContext;

        public TaskRep(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
