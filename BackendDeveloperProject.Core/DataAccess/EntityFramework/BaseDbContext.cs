using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace BackendDeveloperProject.Core.DataAccess.EntityFramework
{

    public class BaseDbContext : DbContext
    {
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public BaseDbContext(DbContextOptions<BaseDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        /// <param name="options"></param>
        /// <param name="configuration"></param>
        protected BaseDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

    }

}
