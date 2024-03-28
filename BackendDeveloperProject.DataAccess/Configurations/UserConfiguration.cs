using BackendDeveloperProject.Core.DataAccess.EntityFramework.Configurations;
using BackendDeveloperProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendDeveloperProject.DataAccess.Configurations
{
    public class UserConfiguration : BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FullName)
                .HasColumnName("FullName")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .HasColumnName("PasswordHash")
                .IsRequired();

            builder.Property(x => x.PasswordSalt)
                .HasColumnName("PasswordSalt")
                .IsRequired();
        }
    }
}
