using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EntityBaseRep<Tkey, T> : IEntityBaseRep<Tkey, T> where T : class, new()
    {
        private readonly AppDbContext dbContext;

        public EntityBaseRep(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll() => dbContext.Set<T>();

        public async Task AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbContext.Set<T>().Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(Tkey id) =>
                await dbContext.Set<T>().FirstOrDefaultAsync(u => ((IIdentifiable<Tkey>)u).Id.Equals(id));

        public async Task SaveChangesAsync() => await dbContext.SaveChangesAsync();
    }
}
