using BackendDeveloperProject.Core.Entities.Abstract;
using BackendDeveloperProject.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendDeveloperProject.Core.DataAccess.EntityFramework.Configurations
{
    public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity, IEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }

}
