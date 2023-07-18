using Application.Services;
using Domain.Entities;

namespace Application.DomainServices
{
    public interface ISolutionRep : IEntityBaseRep<Solution>
    {
        Task<Solution?> GetByIdAsync(int id);
        IEnumerable<Solution> GetAllUnremoved();
        Task DeleteAsync(Solution sol);
    }
}
