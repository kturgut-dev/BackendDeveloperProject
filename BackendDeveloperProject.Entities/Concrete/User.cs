using BackendDeveloperProject.Core.Entities.Concrete;
using BackendDeveloperProject.Entities.Abstract;

namespace BackendDeveloperProject.Entities.Concrete
{
    public class User : Entity, IUser
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
