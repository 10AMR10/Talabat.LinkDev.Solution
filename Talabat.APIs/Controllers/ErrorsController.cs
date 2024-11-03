using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
	[Route("errors/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorsController : ControllerBase
	{
		public ActionResult error(int code)
		{
			if (code == 400)
				return BadRequest(new ApiResponse(code));
			return NotFound(new ApiResponse(code));
		}
	}
}
