using Microsoft.EntityFrameworkCore;

namespace Retrowars.Data.Repository;

public interface IRepository<T> where T : class, IBaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetOneAsync(Guid id);
    Task AddAsync(T entity);
    Task<bool> UpdateOneAsync(T entity);
    Task<bool> DeleteOneAsync(Guid id);
    Task SaveAsync();
}