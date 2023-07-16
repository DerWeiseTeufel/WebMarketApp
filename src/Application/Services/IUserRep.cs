using Application.Interfaces;
using Application.Services;
using Domain.Entities;

namespace Application.DomainServices
{
    public interface IUserRep: IEntityBaseRep<User>
    {
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByEmailAsync(string email);
    }
}
