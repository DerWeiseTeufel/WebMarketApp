using Application.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Tasks
{
    public class GetActiveUseCase
    {
        private readonly ITaskItemRep taskItemRep;

        public GetActiveUseCase(ITaskItemRep taskItemRep)
        {
            this.taskItemRep = taskItemRep;
        }


        public IEnumerable<TaskItem> GetActive()
        {
            return taskItemRep.GetAll().Where(task => 
            task.Deadline > DateTime.Now && task.IsRemoved == false).OrderByDescending(it => it.Id);
        }
    }
}
