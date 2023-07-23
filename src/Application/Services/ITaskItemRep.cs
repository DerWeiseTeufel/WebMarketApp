using Application.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Services
{
    public interface ITaskItemRep : IEntityBaseRep<int, TaskItem>
    {
    }
}
