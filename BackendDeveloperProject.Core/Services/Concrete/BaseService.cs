using BackendDeveloperProject.Core.DataAccess.EntityFramework.Abstract;
using BackendDeveloperProject.Core.Entities.Abstract;
using BackendDeveloperProject.Core.Services.Abstract;
using BackendDeveloperProject.Entities.Concrete;

namespace BackendDeveloperProject.Core.Services.Concrete
{
    public class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : class, IEntity, new()
    {
        public readonly IEntityRepository<TEntity> _repository;
        public BaseService(IEntityRepository<TEntity> service)
        {
            _repository = service;
        }


        public virtual async Task<DataResult<TEntity>> AddAsync(TEntity entity)
        {
            DataResult<TEntity> result = new();
            result.Data = await _repository.AddAsync(entity);
            result.IsSuccess = result.Data != null;
            result.Message = result.IsSuccess ? "Ekleme işlemi başarılı." : "Ekleme işlemi başarısız.";
            return result;
        }

        public virtual async Task<Result> DeleteAsync(long id)
        {
            Result result = new();
            result.IsSuccess = await _repository.DeleteByIdAsync(id);
            result.Message = result.IsSuccess ? "Silme işlemi başarılı." : "Silme işlemi başarısız.";
            return result;
        }

        public virtual async Task<DataResult<TEntity>> GetAsync(long id)
        {
            DataResult<TEntity> result = new();
            result.Data = await _repository.GetByIdAsync(id);
            result.IsSuccess = result.Data != null;
            result.Message = result.IsSuccess ? "Veri getirme işlemi başarılı." : "Veri getirme işlemi başarısız.";
            return result;
        }

        public virtual async Task<DataResult<IEnumerable<TEntity>>> GetListAsync()
        {
            DataResult<IEnumerable<TEntity>> result = new();
            result.Data = await _repository.GetListAsync();
            result.IsSuccess = result.Data != null;
            result.Message = result.IsSuccess ? "Veri getirme işlemi başarılı." : "Veri getirme işlemi başarısız.";
            return result;
        }

        public virtual async Task<Result> UpdateAsync(TEntity entity)
        {
            Result result = new();
            result.IsSuccess = await _repository.UpdateAsync(entity);
            result.Message = result.IsSuccess ? "Güncelleme işlemi başarılı." : "Güncelleme işlemi başarısız.";
            return result;
        }
    }
}
