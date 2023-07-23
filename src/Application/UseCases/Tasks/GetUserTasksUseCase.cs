using Application.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tasks
{
    public class GetUserTasksUseCase
    {
        private readonly ITaskItemRep taskItemRep;

        public GetUserTasksUseCase(ITaskItemRep taskItemRep)
        {
            this.taskItemRep = taskItemRep;
        }


        public IEnumerable<TaskItem> GetUserTasks(string? userId)
        {
            if (userId is null)
            {
                throw new Exception("Invalid input");
            }

            return taskItemRep.GetAll().Where(task =>
                task.CreatorId == userId && task.IsRemoved == false).OrderByDescending(it => it.Id);
        }
    }
}
