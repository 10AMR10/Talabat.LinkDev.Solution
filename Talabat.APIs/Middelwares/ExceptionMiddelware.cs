using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middelwares
{
    }
public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionMiddleware> _logger;
	private readonly IWebHostEnvironment _env;

	public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
	{
		_next = next;
		_logger = logger;
		_env = env;
	}

	public async Task InvokeAsync(HttpContext httpContext)
	{
		try
		{
			await _next(httpContext); // Move to the next middleware
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An unhandled exception occurred."); // Log full exception details

			httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			httpContext.Response.ContentType = "application/json";

			var response = _env.IsDevelopment()
				? new ExceptionErrorApiResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
				: new ExceptionErrorApiResponse((int)HttpStatusCode.InternalServerError);

			var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
			var json = JsonSerializer.Serialize(response, options);

			await httpContext.Response.WriteAsync(json);
		}
	}

}                
