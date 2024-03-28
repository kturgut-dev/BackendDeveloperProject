namespace BackendDeveloperProject.Entities.Abstract
{
    public interface IDataResult<TData> : IResult
    {
        TData Data { get; set; }
    }
}
