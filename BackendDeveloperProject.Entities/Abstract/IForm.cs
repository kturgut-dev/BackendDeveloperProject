using BackendDeveloperProject.Core.Entities.Abstract;
using BackendDeveloperProject.Entities.Concrete;

namespace BackendDeveloperProject.Entities.Abstract
{
    public interface IForm : IAuditEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<FormField>? Fields { get; set; }
    }
}
