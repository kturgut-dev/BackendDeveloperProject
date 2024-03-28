namespace BackendDeveloperProject.Entities.Abstract
{
    public interface IResult
    {
        bool IsSuccess { get; set; }
        int StatusCode { get; set; }
        string Message { get; set; }
        //IEnumerable<string>? Errors { get; set; }

        //void AddError(string error);
        //void AddErrors(IEnumerable<string> errors);
        void SetNotFound(string msg, int statusCode = 404);
        void SetNotFound(int statusCode = 404);
        void SetError(string msg, int statusCode = 500);
        void SetError(int statusCode = 500);
        void SetBadRequest(string msg, int statusCode = 400);
        void SetSuccess(string msg, int statusCode = 200);
        void SetSuccess(int statusCode = 200);
        void SetResult(bool isSuccess, string msg, int statusCode = 200);
    }

}
