using BackendDeveloperProject.Core.DataAccess.EntityFramework.Concrete;
using BackendDeveloperProject.DataAccess.Abstract;
using BackendDeveloperProject.Entities.Concrete;

namespace BackendDeveloperProject.DataAccess.Concrete
{
    public class FormRepository : EfEntityRepositoryBase<Form>, IFormRepository
    {
        public FormRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
