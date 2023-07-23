using Application.Services;
using Domain.Interfaces;

namespace Application.UseCases.Common
{
    public class GetAllUseCase<T, TKey> where T : class, new()
    {
        private readonly Services.IEntityBaseRep<TKey, T> entityBaseRep;

        public GetAllUseCase(IEntityBaseRep<TKey, T> entityBaseRep)
        {
            this.entityBaseRep = entityBaseRep;
        }


        public IEnumerable<T> GetAll()
        {
            if (typeof(IUndeletable).IsAssignableFrom(typeof(T)))
            {
                return entityBaseRep.GetAll().Where(it => 
                    ((IUndeletable)it).IsRemoved == false);
            }

            return entityBaseRep.GetAll();
        }
    }
}
