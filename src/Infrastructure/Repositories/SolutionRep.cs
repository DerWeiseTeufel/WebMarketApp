using Application.DomainServices;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SolutionRep : EntityBaseRep<Solution>, ISolutionRep
    {
        private readonly AppDbContext dbContext;

        public SolutionRep(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Solution?> GetByIdAsync(int id) => 
            await dbContext.Solutions.FirstOrDefaultAsync(a => a.Id == id);        
    }
}
