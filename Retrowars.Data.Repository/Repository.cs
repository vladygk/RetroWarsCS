﻿using Microsoft.EntityFrameworkCore;
using RetroWars.Data;

namespace Retrowars.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly RetroWarsDbContext context;
        private DbSet<T> currentSet;
        public Repository(RetroWarsDbContext context)
        {
            this.context = context;
            this.currentSet = this.context.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await currentSet.ToListAsync();
        }

        public async Task<T?> GetOneAsync(Guid id)
        {
           return await this.currentSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(T entity)
        {
            await this.currentSet.AddAsync(entity);
        }

        public async Task<bool> UpdateOneAsync(T entity)
        {
            T? toUpdate = await this.currentSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (toUpdate is null)
            {
                return false;
            }

            this.currentSet.Update(entity);
            return true;
        }
    

        public async Task<bool> DeleteOneAsync(Guid id)
        {
            T? toDelete = await this.currentSet.FirstOrDefaultAsync(x => x.Id == id);

            if (toDelete is null)
            {
                return false;
            }

            this.currentSet.Remove(toDelete);
            return true;
        }

        public async Task SaveAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}