namespace Dars_5ososy_API.Shared.Helpers
{
    public class ApiResponse<TResult>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public TResult Data { get; set; }

        public static ApiResponse<TResult> Succeeded(TResult data, string message = "Request successful.")
        {
            return new ApiResponse<TResult>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<TResult> Fail(string message)
        {
            return new ApiResponse<TResult>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }
}
