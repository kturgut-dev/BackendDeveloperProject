using System.ComponentModel.DataAnnotations;

namespace BackendDeveloperProject.Entities.Concrete
{
    public class FormField
    {
        public bool Required { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? DataType { get; set; }
    }
}
