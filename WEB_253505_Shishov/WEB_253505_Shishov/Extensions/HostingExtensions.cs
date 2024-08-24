using WEB_253505_Shishov.Services.CategoryService;
using WEB_253505_Shishov.Services.ConstructorService;

namespace WEB_253505_Shishov.Extensions;

public static class HostingExtensions
{
	public static void RegisterCustomServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddScoped<ICategoryService, MemoryCategoryService>()
						.AddScoped<IConstructorService, MemoryConstructorService>();
	}
}
