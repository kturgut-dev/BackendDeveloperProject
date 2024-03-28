using BackendDeveloperProject.Core.Entities.Abstract;
using System.Linq.Expressions;

namespace BackendDeveloperProject.Core.DataAccess.EntityFramework.Abstract
{
    public interface IEntityRepository<T> where T : class, IEntity
    {
        T Add(T entity);
        Task<T> AddAsync(T entity);
        bool Update(T entity);
        Task<bool> UpdateAsync(T entity);
        bool Delete(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteByIdAsync(long id);
        bool DeleteById(long id);
        T GetById(long id);
        Task<T> GetByIdAsync(long id);
        T Get(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetList(Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? expression = null);
    }
}
