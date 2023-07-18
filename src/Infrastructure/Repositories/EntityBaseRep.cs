using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EntityBaseRep<T> : IEntityBaseRep<T> where T : class, new()
    {
        private readonly AppDbContext dbContext;

        public EntityBaseRep(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteAsync(T entity, bool onlyTag)
        {
            if (onlyTag)
            {
                ((IUndeletable)entity).IsRemoved = true;
                dbContext.Set<T>().Update(entity);
            }
            else
            {
                dbContext.Set<T>().Remove(entity);
            }
         
            await dbContext.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>();
        }

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
    }
}
