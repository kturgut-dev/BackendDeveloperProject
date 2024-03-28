using BackendDeveloperProject.Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace BackendDeveloperProject.Core.Entities.Concrete
{
    public class Entity : IEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
