using Microsoft.EntityFrameworkCore;
using WEB_253505_Shishov.API.Data;
using WEB_253505_Shishov.API.Services.CategoryService;
using WEB_253505_Shishov.API.Services.ConstructorService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => 
	opt.UseNpgsql(builder.Configuration.GetConnectionString("ConstructorsDb")));

builder.Services.AddScoped<IConstructorService, ConstructorService>()
				.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

var pendingMigrations = context.Database.GetPendingMigrations();

if (pendingMigrations.Any())
{
	await context.Database.MigrateAsync();
}

//await DbInitializer.SeedData(app);

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();

app.Run();
