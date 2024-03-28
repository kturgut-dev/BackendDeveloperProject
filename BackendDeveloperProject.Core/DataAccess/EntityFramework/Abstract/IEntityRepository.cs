using BackendDeveloperProject.Core.Entities.Abstract;
using System.Linq.Expressions;

namespace BackendDeveloperProject.Core.DataAccess.EntityFramework.Abstract
{
    public interface IEntityRepository<T> where T : class, IEntity
    {
        T Add(T entity);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        bool Update(T entity);
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        bool Delete(T entity);
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<bool> DeleteByIdAsync(long id, CancellationToken cancellationToken = default);
        bool DeleteById(long id);
        T GetById(long id);
        Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        T Get(Expression<Func<T, bool>> expression);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        IEnumerable<T> GetList(Expression<Func<T, bool>> expression = null);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? expression = null, CancellationToken cancellationToken = default);
    }
}
