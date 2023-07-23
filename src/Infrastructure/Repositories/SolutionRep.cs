using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SolutionRep : EntityBaseRep<int, Solution>, ISolutionRep
    {
        private readonly AppDbContext dbContext;

        public SolutionRep(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
