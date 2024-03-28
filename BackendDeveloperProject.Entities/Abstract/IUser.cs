using BackendDeveloperProject.Core.Entities.Abstract;

namespace BackendDeveloperProject.Entities.Abstract
{
    public interface IUser : IEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
