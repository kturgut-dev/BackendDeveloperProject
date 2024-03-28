using BackendDeveloperProject.Entities.Abstract;

namespace BackendDeveloperProject.Entities.Concrete
{
    public class DataResult<TData> : Result, IDataResult<TData>
    {
        public TData Data { get; set; }

        public DataResult()
        {

        }

        public DataResult(bool isSuccess, string msg) : base(isSuccess, msg)
        {

        }
    }

}
