namespace BackendDeveloperProject.Entities.Abstract
{
    public interface IFormField
    {
        public bool Required { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
    }
}
