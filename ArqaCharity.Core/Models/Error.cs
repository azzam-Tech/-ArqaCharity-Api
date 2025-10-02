namespace ArqaCharity.Core.Models
{
    public class Error
    {
        public string Code { get; }
        public string Message { get; }
        public int StatusCode { get; }

        public Error(string code, string message, int statusCode = 500)
        {
            Code = code;
            Message = message;
            StatusCode = statusCode;
        }

        public static Error None => new Error(string.Empty, string.Empty, 200);
    }
}
