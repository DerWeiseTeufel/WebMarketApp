using Application.Services;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases.Solutions
{
    public class DeclineSolUseCase
    {
        private readonly IUserRep userRep;
        private readonly ISolutionRep solRep;

        public DeclineSolUseCase(IUserRep userRep, ISolutionRep solRep)
        {
            this.userRep = userRep;
            this.solRep = solRep;
        }


        public async Task DeclineSolAsync(Solution sol)
        {
            if (await solRep.GetByIdAsync(sol.Id) is null)
            {
                throw new Exception("Solution not found");
            }

            sol.Status = SolutionStatuses.Rejected.ToString();
            await solRep.UpdateAsync(sol);

            var executor = await userRep.GetByIdAsync(sol.ExecutorId);
            if (executor is null)
            {
                throw new Exception("Executor not found");
            }

            executor.Reputation += Reputation.Rejected;
            await userRep.UpdateAsync(executor);
        }
    }
}
