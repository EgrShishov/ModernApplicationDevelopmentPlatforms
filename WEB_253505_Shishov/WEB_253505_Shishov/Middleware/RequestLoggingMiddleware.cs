using Microsoft.AspNetCore.Http.Extensions;
using Serilog;

namespace WEB_253505_Shishov.Middleware;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class RequestLoggingMiddleware
{
	private readonly RequestDelegate _next;

	public RequestLoggingMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext httpContext)
	{
		await _next(httpContext);

		if (httpContext.Response.StatusCode < 200 || httpContext.Response.StatusCode >= 300)
		{
			Log.Error($"request {httpContext.Request.GetDisplayUrl()} returns {httpContext.Response.StatusCode}");
		}
	}
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class RequestLogginMiddlewareExtensions
{
	public static IApplicationBuilder UseRequestLogginMiddleware(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<RequestLoggingMiddleware>();
	}
}
