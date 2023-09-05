using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public bool Success { get; }

        public string Message { get; }
        public Result(bool Success, string Message) : this(Success)
        {
            this.Message = Message;
        }
        public Result(bool Success)
        {
            this.Success = Success;
        }
    }
}
