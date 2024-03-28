using BackendDeveloperProject.Core.Entities.Concrete;
using BackendDeveloperProject.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace BackendDeveloperProject.Entities.Concrete
{
    public class Form : AuditEntity, IForm
    {
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IList<FormField>? Fields { get; set; }
    }
}
