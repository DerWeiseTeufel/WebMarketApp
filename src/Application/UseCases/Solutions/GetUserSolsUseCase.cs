using Application.Services;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases.Solutions
{
    public class GetUserSolsUseCase
    {
        private readonly IUserRep userRep;
        private readonly ISolutionRep solRep;

        public GetUserSolsUseCase(IUserRep userRep, ISolutionRep solRep)
        {
            this.userRep = userRep;
            this.solRep = solRep;
        }


        public async Task<IEnumerable<Solution>> GetUserSolsAsync(string? userId)
        {
            if (userId is null)
            {
                throw new Exception("Invalid input");
            }

            var user = await userRep.GetByIdAsync(userId);
            if (user is null)
            {
                throw new Exception("Executor not found");
            }            

            return solRep.GetAll().Where(sol => 
                sol.ExecutorId == userId && sol.IsRemoved == false).OrderByDescending(it => it.Id);
        }
    }
}
