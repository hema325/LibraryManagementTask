namespace LibraryManagementTask.Errors
{
    public class ErrorResponse
    {
        public int StatusCode { get; }
        public string? Message { get; }

        public ErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? DefaultErrorMessage(statusCode);
        }

        private string? DefaultErrorMessage(int statusCode) => statusCode switch
        {
            404 => "The requested resource was not found.",
            401 => "You are unauthorized to access this resource.",
            403 => "You don't have permission to access this resource.",
            400 => "Validation failed. Please check your input data.",
            _ => null
        };

        public static ErrorResponse NotFound()
        => new ErrorResponse(StatusCodes.Status404NotFound);    
        
        public static ErrorResponse Unauthorized(string? message= null)
        => new ErrorResponse(StatusCodes.Status404NotFound, message);

    }
}
