using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middelwares
{
	public class ExceptionMiddelware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddelware> _logger;
		private readonly IWebHostEnvironment _env;

		public ExceptionMiddelware(RequestDelegate next,ILogger<ExceptionMiddelware> logger,IWebHostEnvironment env)
        {
			_next = next;
			_logger = logger;
			_env = env;
		}
		public  async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
					await _next.Invoke(httpContext); // this go two next middelware
			}
			catch (Exception ex )
			{

				_logger.LogError(ex.Message); // log error in console

				httpContext.Response.StatusCode=(int) HttpStatusCode.InternalServerError; // header
				httpContext.Response.ContentType = "application/json"; // header
				var response = _env.IsDevelopment() ? new ExceptionErrorApiResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
					: new ExceptionErrorApiResponse((int)HttpStatusCode.InternalServerError);
				var options= new JsonSerializerOptions() {PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
				var json= JsonSerializer.Serialize(response,options);
				

				await httpContext.Response.WriteAsync(json);
			}
		}
    }
}                
