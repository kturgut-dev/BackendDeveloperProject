using BackendDeveloperProject.Core.DataAccess.EntityFramework.Abstract;
using BackendDeveloperProject.Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace BackendDeveloperProject.Core.DataAccess.EntityFramework.Concrete
{

    /// <summary>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> //, TContext
        where TEntity : class, IEntity
        //where TContext : DbContext
    {
        protected DbContext _context { get; }
        protected DbSet<TEntity> _dbSet { get; }

        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Added;
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Added;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                return default;
            }
        }


        public bool Update(TEntity entity)
        {
            try
            {
                using (_context)
                {
                    TEntity oldRec = Get(x => x.Id == entity.Id);
                    foreach (PropertyInfo property in typeof(TEntity).GetProperties().Where(p => p.CanWrite))
                        if (property.GetValue(entity, null) is not null)
                            property.SetValue(oldRec, property.GetValue(entity, null), null);
                    _context.Entry(oldRec).State = EntityState.Modified;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            try
            {
                using (_context)
                {
                    TEntity oldRec = Get(x => x.Id == entity.Id);
                    foreach (PropertyInfo property in typeof(TEntity).GetProperties().Where(p => p.CanWrite))
                        if (property.GetValue(entity, null) is not null)
                            property.SetValue(oldRec, property.GetValue(entity, null), null);
                    _context.Entry(oldRec).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(expression);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AsNoTracking().AsQueryable().FirstOrDefaultAsync(expression);
        }

        public TEntity GetById(long id)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _dbSet.AsNoTracking().AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
        }


        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null
                ? _dbSet.AsNoTracking()
                : _dbSet.AsNoTracking().Where(expression);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression == null
                ? await _dbSet.AsNoTracking().ToListAsync()
                : await _dbSet.AsNoTracking().Where(expression).ToListAsync();
        }


        public bool Delete(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Deleted;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(long id)
        {
            try
            {
                TEntity entity = await GetAsync(x => x.Id == id);
                if (entity == null) return false;
                _context.Entry(entity).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteById(long id)
        {
            try
            {
                TEntity entity = Get(x => x.Id == id);
                if (entity == null) return false;
                _context.Entry(entity).State = EntityState.Deleted;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
