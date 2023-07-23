using Application.Interfaces;
using Application.Services;
using Domain.Entities;

namespace Application.Services
{
    public interface IUserRep: IEntityBaseRep<string, User>
    {
    }
}
