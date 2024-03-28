using BackendDeveloperProject.Core.Entities.Abstract;

namespace BackendDeveloperProject.Core.Entities.Concrete
{
    public class AuditEntity : Entity, IAuditEntity
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
