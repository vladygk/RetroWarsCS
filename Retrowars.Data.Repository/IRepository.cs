using Microsoft.EntityFrameworkCore;

namespace Retrowars.Data.Repository;

public interface IRepository<T> where T : class, IBaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetOneAsync(string id, bool isEmail);
    Task<bool> AddAsync(T entity);
    Task<bool> UpdateOneAsync(T entity);
    Task<bool> DeleteOneAsync(Guid id);
    Task<bool> SaveAsync();
}