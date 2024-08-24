using Microsoft.Extensions.Options;
using WEB_253505_Shishov.Extensions;
using WEB_253505_Shishov.Helpers;
using WEB_253505_Shishov.Services.CategoryService;
using WEB_253505_Shishov.Services.ConstructorService;
using WEB_253505_Shishov.Services.FileService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.RegisterCustomServices();

builder.Services.Configure<UriData>(builder.Configuration.GetSection("UriData"));

builder.Services.AddScoped<ICategoryService, ApiCategoryService>()
                .AddScoped<IConstructorService, ApiConstructorService>()
                .AddScoped<IFileService, ApiFileService>();

builder.Services.AddHttpClient("api", client =>
{
    var uriData = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<UriData>>().Value;
    client.BaseAddress = new Uri(uriData.ApiUri);
});

builder.Services.AddHttpClient("filesapi", client =>
{
    var uriData = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<UriData>>().Value;
    client.BaseAddress = new Uri($"{uriData.ApiUri}Files");
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
