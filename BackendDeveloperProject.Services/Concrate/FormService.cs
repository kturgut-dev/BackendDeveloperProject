using BackendDeveloperProject.Core.Services.Concrete;
using BackendDeveloperProject.DataAccess.Abstract;
using BackendDeveloperProject.Entities.Concrete;
using BackendDeveloperProject.Services.Abstract;

namespace BackendDeveloperProject.Services.Concrate
{
    public class FormService : BaseService<Form>, IFormService
    {
        private readonly IFormRepository _modelRepository;
        public FormService(IFormRepository modelRepository) : base(modelRepository)
        {
            _modelRepository = modelRepository;
        }
    }
}
