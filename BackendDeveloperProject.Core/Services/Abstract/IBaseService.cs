using BackendDeveloperProject.Core.Entities.Abstract;
using BackendDeveloperProject.Entities.Concrete;

namespace BackendDeveloperProject.Core.Services.Abstract;

public interface IBaseService<TEntity>
     where TEntity : class, IEntity, new()
{
    Task<DataResult<TEntity>> AddAsync(TEntity entity);
    Task<Result> UpdateAsync(TEntity entity);
    Task<Result> DeleteAsync(long id);
    Task<DataResult<TEntity>> GetAsync(long id);
    Task<DataResult<IEnumerable<TEntity>>> GetListAsync();
}
