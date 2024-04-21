namespace LibraryManagementTask.Errors
{
    public class ExceptionResponse : ErrorResponse
    {
        public string? Details { get; set; }
        public ExceptionResponse(string? message, string? details = null) : base(StatusCodes.Status500InternalServerError, message)
        {
            Details = details;
        }
    }
}
