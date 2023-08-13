using Microsoft.EntityFrameworkCore;
using RetroWars.Data;

namespace RetroWars.Data.Repository
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

        public async Task<T?> GetOneAsync(string id, bool isEmail)
        {
            if (isEmail)
            {
                foreach (var entity in this.currentSet)
                {
                    string email = this.GetEmail(entity);

                    if (email == id)
                    {
                        return entity;
                    }
                }

                return null;
            }
            return await this.currentSet.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
        }

        private string GetEmail(object obj)
        {
            return (string)obj.GetType().GetProperty("Email")?.GetValue(obj, null);
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                await this.currentSet.AddAsync(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateOneAsync(T entity)
        {
            try
            {
                T? toUpdate = await this.currentSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (toUpdate is null)
                {
                    return false;
                }

                this.currentSet.Remove(toUpdate);
                await this.currentSet.AddAsync(entity);

                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> DeleteOneAsync(Guid id)
        {
            try
            {
                T? toDelete = await this.currentSet.FirstOrDefaultAsync(x => x.Id == id);

                if (toDelete is null)
                {
                    return false;
                }

                this.currentSet.Remove(toDelete);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await this.context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}