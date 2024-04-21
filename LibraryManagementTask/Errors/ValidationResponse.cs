namespace LibraryManagementTask.Errors
{
    public class ValidationResponse : ErrorResponse
    {
        public IEnumerable<string> Errors { get; }

        public ValidationResponse(IEnumerable<string> errors, string? message = null) : base(StatusCodes.Status400BadRequest, message)
        {
            Errors = errors;
        }
    }
}
