namespace Dars_5ososy_API.Shared.Helpers
{
    public class ApiResponse<TResult>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public TResult? Data { get; set; }
        public List<string>? Errors { get; set; }
        public string TraceId { get; set; } = Guid.NewGuid().ToString();

        // Factory Methods for clean controller invocation
        public static ApiResponse<TResult> Success<TResult>(TResult data, string message = "Request successful.") => 
            new() { IsSuccess = true, Data = data, Message = message };

        public static ApiResponse<TResult> Failure(List<string> errors, string message = "Operation failed.") => 
            new() { IsSuccess = false, Message = message, Errors = errors };

        public static ApiResponse<TResult> Failure(string error, string message = "An error occurred.") => 
            new() { IsSuccess = false, Message = message, Errors = new List<string> { error } };
    }
}
