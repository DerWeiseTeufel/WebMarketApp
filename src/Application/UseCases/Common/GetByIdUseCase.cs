using Application.Services;
using Domain.Interfaces;

namespace Application.UseCases.Common
{
    public class GetByIdUseCase<T, TKey> where T : class, new()
    {
        private readonly Services.IEntityBaseRep<TKey, T> entityBaseRep;

        public GetByIdUseCase(IEntityBaseRep<TKey, T> entityBaseRep)
        {
            this.entityBaseRep = entityBaseRep;
        }


        public async Task<T?> GetByIdAsync(TKey id)
        {
            var tmpEntity = await entityBaseRep.GetByIdAsync(id);

            if (tmpEntity != null && typeof(IUndeletable).IsAssignableFrom(typeof(T)))
            {
                return ((IUndeletable)(tmpEntity)).IsRemoved ? null : tmpEntity;
            }

            return tmpEntity;
        }
    }
}
