using Application.DomainServices;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRep : EntityBaseRep<User>, IUserRep
    {
        private readonly AppDbContext dbContext;

        public UserRep(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User?> GetByIdAsync(string id) 
            => await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User?> GetByEmailAsync(string email)
           => await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
