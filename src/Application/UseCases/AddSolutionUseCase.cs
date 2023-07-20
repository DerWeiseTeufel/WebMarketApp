using Application.Services;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCases
{
    public class AddSolutionUseCase : IAddSolutionUseCase
    {
        private readonly IUserRep userRep;
        private readonly ITaskItemRep taskRep;
        private readonly ISolutionRep solRep;

        public AddSolutionUseCase(IUserRep userRep, ITaskItemRep taskRep, ISolutionRep solRep)
        {
            this.userRep = userRep;
            this.solRep = solRep;
            this.taskRep = taskRep;
        }


        public async Task<string?> AddSolution(int taskId, string? userId, string? URL)
        {
            try
            {
                if (userId is null || URL is null)
                {
                    throw new Exception("Invalid input");
                }

                var user = await userRep.GetByIdAsync(userId);
                if (user is null)
                {
                    throw new Exception("User not found");
                }

                var task = await taskRep.GetByIdAsync(taskId);
                if (task is null)
                {
                    throw new Exception("Task not found");
                }

                var newSol = new Solution()
                {
                    TaskItemId = taskId,
                    URL = URL,
                    ExecutorId = userId,
                    UploadDate = DateTime.Now,
                    Status = SolutionStatuses.UnderReview.ToString(),
                    Comment = ""
                };

                await solRep.AddAsync(newSol);

                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
