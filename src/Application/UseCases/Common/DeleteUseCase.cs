using Application.Services;
using Domain.Interfaces;

namespace Application.UseCases.Common
{
    public class DeleteUseCase<T, TKey> where T : class, new()
    {
        private readonly Services.IEntityBaseRep<TKey, T> entityBaseRep;

        public DeleteUseCase(IEntityBaseRep<TKey, T> entityBaseRep)
        {
            this.entityBaseRep = entityBaseRep;
        }


        public async Task DeleteByIdAsync(TKey id)
        {
            var entity = await entityBaseRep.GetByIdAsync(id);
            if (entity is null)
            {
                throw new Exception("Item not found");
            }

            if (entity is IUndeletable)
            {
                ((IUndeletable)entity).IsRemoved = true;
                await entityBaseRep.UpdateAsync(entity);
            }
            else
            {
                await entityBaseRep.DeleteAsync(entity);
            }
        }
    }
}
