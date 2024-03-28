using BackendDeveloperProject.Core.Services.Abstract;
using BackendDeveloperProject.Entities.Concrete;
using BackendDeveloperProject.Entities.DataTransferObjects.Request;

namespace BackendDeveloperProject.Services.Abstract
{
    public interface IUserService : IBaseService<User>
    {
        Task<Result> Login(UserLoginForm formData, CancellationToken cancellationToken = default);
        Task<Result> Logout();
    }
}
