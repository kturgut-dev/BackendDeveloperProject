using BackendDeveloperProject.Core.DataAccess.EntityFramework.Abstract;
using BackendDeveloperProject.Core.DataAccess.EntityFramework.Concrete;
using BackendDeveloperProject.Core.Helpers;
using BackendDeveloperProject.Core.Services.Abstract;
using BackendDeveloperProject.Core.Services.Concrete;
using BackendDeveloperProject.DataAccess;
using BackendDeveloperProject.DataAccess.Abstract;
using BackendDeveloperProject.DataAccess.Concrete;
using BackendDeveloperProject.DataAccess.Seed;
using BackendDeveloperProject.Services.Abstract;
using BackendDeveloperProject.Services.Concrate;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace BackendDeveloperProject
{
    public class ServicesStartup
    {
        protected IConfiguration _configuration { get; }
        protected IHostEnvironment _hostEnvironment { get; }

        public ServicesStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }

        private void Initiliaze(IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityRepository<>), typeof(EfEntityRepositoryBase<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IFormRepository, FormRepository>();
            services.AddScoped<IFormService, FormService>();

            // dotnet ef migrations add mig-changes --context ProjectDbContext --project BackendDeveloperProject
            // dotnet ef database update --context ProjectDbContext --project BackendDeveloperProject
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromHours(8);
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/";
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
            });

            // add-migration user-db-init -context BlogBuilderContext
            services.AddDbContext<ProjectDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                      b => b.MigrationsAssembly("BackendDeveloperProject.UI"));
            });

            services.AddAutoMapper(typeof(ServicesStartup).Assembly);

            Initiliaze(services);
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                UserDataSeeding.Seed(app);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Her yerden ulaşabilmek için.
            ServiceTool.ServiceProvider = app.Services;
            ServiceTool.Configuration = app.Configuration;
            ServiceTool.HttpContextAccessor = app.Services.GetService<IHttpContextAccessor>();
            ServiceTool.ServiceProvider = app.Services;
        }
    }
}
