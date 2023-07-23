using Application.Services;
using Application.UseCases.Common;
using Application.UseCases.Tasks;
using Application.UseCases.Users;
using Domain.Entities;

namespace Application.UseCases
{
    public class TaskUseCases
    {
        public readonly DeleteUseCase<TaskItem, int> Delete;
        public readonly GetAllUseCase<TaskItem, int> GetAll;
        public readonly UpdateUseCase<TaskItem, int> Update;        
        public readonly GetByIdUseCase<TaskItem, int> GetById;
        public readonly GetUserTasksUseCase GetUserTasks;
        public readonly SaveTaskUseCase Add;
        public readonly GetActiveUseCase GetActive;

        public TaskUseCases(IEntityBaseRep<int, TaskItem> entityBaseRep, IUserRep userRep, ITaskItemRep taskItemRep)
        {
            Delete = new(entityBaseRep);
            GetAll = new(entityBaseRep);
            GetById = new(entityBaseRep);
            Add = new(userRep, taskItemRep);
            GetActive = new(taskItemRep);
            Update = new(entityBaseRep);
            GetUserTasks = new(taskItemRep);
        }
    }
}
