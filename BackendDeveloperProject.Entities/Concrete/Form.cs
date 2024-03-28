using BackendDeveloperProject.Core.Entities.Concrete;
using BackendDeveloperProject.Entities.Abstract;

namespace BackendDeveloperProject.Entities.Concrete
{
    public class Form : AuditEntity, IForm
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<FormField>? Fields { get; set; }
    }
}
