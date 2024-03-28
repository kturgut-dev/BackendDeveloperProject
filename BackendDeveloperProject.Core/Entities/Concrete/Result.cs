using IResult = BackendDeveloperProject.Entities.Abstract.IResult;

namespace BackendDeveloperProject.Entities.Concrete
{

    public class Result : Abstract.IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        //public IEnumerable<string>? Errors { get; set; }

        public Result()
        {
            StatusCode = 200;
        }

        public Result(bool isSuccess, string msg) : this()
        {
            IsSuccess = isSuccess;
            Message = msg;
        }

        //public void AddError(string error)
        //{
        //    if (Errors == null)
        //        Errors = new List<string>();

        //    IsSuccess = false;

        //    ((List<string>)Errors).Add(error);
        //}

        //public void AddErrors(IEnumerable<string> errors)
        //{
        //    if (Errors == null)
        //        Errors = new List<string>();

        //    IsSuccess = false;

        //    ((List<string>)Errors).AddRange(errors);
        //}

        public void SetNotFound(string msg, int statusCode = 404)
        {
            Message = msg;
            StatusCode = statusCode;
            IsSuccess = false;
        }

        public void SetNotFound(int statusCode = 404)
        {
            StatusCode = statusCode;
            IsSuccess = false;
        }

        public void SetError(string msg, int statusCode = 500)
        {
            Message = msg;
            StatusCode = statusCode;
            IsSuccess = false;
        }

        public void SetError(int statusCode = 500)
        {
            StatusCode = statusCode;
            IsSuccess = false;
        }

        public void SetSuccess(string msg, int statusCode = 200)
        {
            Message = msg;
            StatusCode = statusCode;
            IsSuccess = true;
        }

        public void SetSuccess(int statusCode = 200)
        {
            StatusCode = statusCode;
            IsSuccess = true;
        }

        public void SetBadRequest(string msg, int statusCode = 400)
        {
            Message = msg;
            StatusCode = statusCode;
            IsSuccess = false;
        }

        public void SetResult(bool isSuccess, string msg, int statusCode = 200)
        {
            Message = msg;
            StatusCode = statusCode;
            IsSuccess = isSuccess;
        }
    }
}
