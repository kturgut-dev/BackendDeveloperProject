namespace BackendDeveloperProject.Core.Entities.Abstract
{
    public interface IAuditEntity : IEntity
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
