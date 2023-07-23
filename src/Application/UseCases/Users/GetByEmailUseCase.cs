using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Users
{
    public class GetByEmailUseCase
    {
        private readonly IUserRep userRep;

        public GetByEmailUseCase(IUserRep userRep)
        {
            this.userRep = userRep;
        }


        public async Task<User?> GetByEmailAsync(string email) => 
            await userRep.GetAll().FirstOrDefaultAsync(u => u.Email == email && u.IsRemoved == false);
    }
}
