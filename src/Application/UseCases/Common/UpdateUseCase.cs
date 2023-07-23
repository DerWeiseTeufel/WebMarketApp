using Application.Services;
using Domain.Interfaces;

namespace Application.UseCases.Common
{
    public class UpdateUseCase<T, TKey> where T : class, new()
    {
        private readonly Services.IEntityBaseRep<TKey, T> entityBaseRep;

        public UpdateUseCase(IEntityBaseRep<TKey, T> entityBaseRep)
        {
            this.entityBaseRep = entityBaseRep;
        }


        public async Task UpdateAsync(T entity)
        {
            await entityBaseRep.UpdateAsync(entity);
        }
    }
}
