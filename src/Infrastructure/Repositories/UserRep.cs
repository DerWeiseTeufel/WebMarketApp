using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRep : EntityBaseRep<string, User>, IUserRep
    {
        private readonly AppDbContext dbContext;

        public UserRep(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }    
    }
}
