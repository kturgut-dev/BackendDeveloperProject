using BackendDeveloperProject.Core.Entities.Abstract;
using BackendDeveloperProject.Entities.Concrete;
using System.Linq.Expressions;

namespace BackendDeveloperProject.Core.Services.Abstract;

public interface IBaseService<TEntity>
     where TEntity : class, IEntity, new()
{
    Task<DataResult<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<DataResult<TEntity>> GetAsync(long id, CancellationToken cancellationToken = default);
    Task<DataResult<IEnumerable<TEntity>>> GetListAsync(CancellationToken cancellationToken = default);
    Task<DataResult<IEnumerable<TEntity>>> GetListAsync(Expression<Func<TEntity, bool>>? expression = null, CancellationToken cancellationToken = default);
}
