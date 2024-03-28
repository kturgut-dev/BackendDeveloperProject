using BackendDeveloperProject.Core.DataAccess.EntityFramework.Abstract;
using BackendDeveloperProject.Core.Entities.Abstract;
using BackendDeveloperProject.Core.Entities.Concrete;
using BackendDeveloperProject.Core.Extensions;
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

        public virtual async Task<DataResult<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            DataResult<TEntity> result = new();
            if (entity is IAuditEntity)
            {
                ((IAuditEntity)entity).CreatedAt = DateTime.Now;
                ((IAuditEntity)entity).CreatedBy = UserInfoExtensions.GetUserId() ?? 0;
            }

            result.Data = await _repository.AddAsync(entity, cancellationToken);
            result.IsSuccess = result.Data != null;
            result.Message = result.IsSuccess ? "Ekleme işlemi başarılı." : "Ekleme işlemi başarısız.";
            return result;
        }

        public virtual async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken = default)
        {
            Result result = new();
            result.IsSuccess = await _repository.DeleteByIdAsync(id, cancellationToken);
            result.Message = result.IsSuccess ? "Silme işlemi başarılı." : "Silme işlemi başarısız.";
            return result;
        }

        public virtual async Task<DataResult<TEntity>> GetAsync(long id, CancellationToken cancellationToken = default)
        {
            DataResult<TEntity> result = new();
            result.Data = await _repository.GetByIdAsync(id, cancellationToken);
            result.IsSuccess = result.Data != null;
            result.Message = result.IsSuccess ? "Veri getirme işlemi başarılı." : "Veri getirme işlemi başarısız.";
            return result;
        }

        public virtual async Task<DataResult<IEnumerable<TEntity>>> GetListAsync(CancellationToken cancellationToken = default)
        {
            DataResult<IEnumerable<TEntity>> result = new();
            result.Data = await _repository.GetListAsync(cancellationToken: cancellationToken);
            result.IsSuccess = result.Data != null;
            result.Message = result.IsSuccess ? "Veri getirme işlemi başarılı." : "Veri getirme işlemi başarısız.";
            return result;
        }

        public virtual async Task<Result> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Result result = new();
            result.IsSuccess = await _repository.UpdateAsync(entity, cancellationToken);
            result.Message = result.IsSuccess ? "Güncelleme işlemi başarılı." : "Güncelleme işlemi başarısız.";
            return result;
        }
    }
}
