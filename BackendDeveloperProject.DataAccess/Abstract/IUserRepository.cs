using BackendDeveloperProject.Core.DataAccess.EntityFramework.Abstract;
using BackendDeveloperProject.Entities.Concrete;

namespace BackendDeveloperProject.DataAccess.Abstract;

public interface IUserRepository : IEntityRepository<User>
{

}