using BackendDeveloperProject.Core.Helpers.Security;
using BackendDeveloperProject.Entities.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BackendDeveloperProject.DataAccess.Seed
{
    public static class UserDataSeeding
    {
        public static async void Seed(IApplicationBuilder app)
        {
            IServiceScope? scope = app.ApplicationServices.CreateScope();
            ProjectDbContext? context = scope.ServiceProvider.GetService<ProjectDbContext>();
            if (context == null) return;
            await context?.Database.MigrateAsync()!;

            if (!context.Users.Any())
            {
                SecurePasswordHasher passwordHasher = new();
                User usrAdmin = new User
                {
                    FullName = "Defaul Admin Account",
                    Email = "admin@mail.com",
                    PasswordHash = passwordHasher.Hash("root"),
                    PasswordSalt = passwordHasher.UserSalt,
                };
                context.Users.Add(usrAdmin);
            }

            context?.SaveChanges();
        }
    }
}
