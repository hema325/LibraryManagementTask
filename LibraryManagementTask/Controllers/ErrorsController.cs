using LibraryManagementTask.Errors;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementTask.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : Controller
    {
        public IActionResult HandleErrors(int statusCode)
        {
            return StatusCode(statusCode, new ErrorResponse(statusCode));
        }
    }
}
