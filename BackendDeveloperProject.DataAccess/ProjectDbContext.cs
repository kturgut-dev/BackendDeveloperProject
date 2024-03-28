using BackendDeveloperProject.Core.DataAccess.EntityFramework;
using BackendDeveloperProject.DataAccess.Configurations;
using BackendDeveloperProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BackendDeveloperProject.DataAccess
{
    public class ProjectDbContext : BaseDbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options, IConfiguration configuration) : base(options, configuration)
        {

        }

        protected ProjectDbContext(DbContextOptions options, IConfiguration configuration) : base(options, configuration)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FormConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .EnableSensitiveDataLogging());
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Form> Forms { get; set; }

    }
}
