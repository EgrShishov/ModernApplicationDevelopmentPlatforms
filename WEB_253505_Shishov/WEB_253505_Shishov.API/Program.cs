using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using WEB_253505_Shishov.API.Data;
using WEB_253505_Shishov.API.Models;
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

var authService = builder.Configuration.GetSection("AuthServer").Get<AuthServerData>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
	{
		opt.Authority = $"{authService.Host}/realms/{authService.Realm}";

		opt.MetadataAddress = $"{authService.Host}/realms/{authService.Realm}/.well-known/openid-configuration";

		opt.Audience = "account";

		opt.RequireHttpsMetadata = false;
	});

builder.Services.AddAuthorization(opt =>
{
	opt.AddPolicy("admin", p => p.RequireRole("POWER-USER"));
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();

app.Run();
