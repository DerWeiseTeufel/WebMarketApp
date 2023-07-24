using Application.Services;
using Domain.Entities;

namespace Application.UseCases.Tasks
{
    public class SaveTaskUseCase
    {
        private readonly IUserRep userRep;
        private readonly ITaskItemRep taskRep;

        public SaveTaskUseCase(IUserRep userRep, ITaskItemRep taskRep)
        {
            this.userRep = userRep;
            this.taskRep = taskRep;
        }


        public async Task AddTaskAsync(TaskItem? taskItem)
        {
            if (taskItem is null)
            {
                throw new Exception("Invalid input");
            }

            var creator = await userRep.GetByIdAsync(taskItem.CreatorId);
            if (creator is null)
            {
                throw new Exception("Creator not found");
            }
           
            await taskRep.AddAsync(taskItem);
        }
    }
}
