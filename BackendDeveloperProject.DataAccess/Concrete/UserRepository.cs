using BackendDeveloperProject.Core.DataAccess.EntityFramework.Concrete;
using BackendDeveloperProject.DataAccess.Abstract;
using BackendDeveloperProject.Entities.Concrete;

namespace BackendDeveloperProject.DataAccess.Concrete;

public class UserRepository : EfEntityRepositoryBase<User>, IUserRepository
{
    public UserRepository(ProjectDbContext context) : base(context)
    {
    }
}
