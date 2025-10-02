using System.Text.Json.Serialization;

namespace ArqaCharity.Core.Models
{
    public class ApiResponse<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = null!;
        public T Data { get; set; } = default!;
        public List<string> Errors { get; set; } = new();
        public int StatusCode { get; set; }

        [JsonConstructor]
        public ApiResponse() { }

        public static ApiResponse<T> Success(T data, string message = "Operation succeeded.")
        {
            return new ApiResponse<T>
            {
                Succeeded = true,
                Message = message,
                Data = data,
                StatusCode = 200
            };
        }

        public static ApiResponse<T> Failure(string message, int statusCode = 500, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Succeeded = false,
                Message = message,
                Errors = errors ?? new List<string> { message },
                StatusCode = statusCode
            };
        }
    }
}
