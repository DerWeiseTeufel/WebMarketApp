using Application.Services;
using Domain.Entities;

namespace Application.Services
{
    public interface ISolutionRep : IEntityBaseRep<Solution>
    {
        Task<Solution?> GetByIdAsync(int id);
        IEnumerable<Solution> GetAllUnremoved();
        Task DeleteAsync(Solution sol);
    }
}
