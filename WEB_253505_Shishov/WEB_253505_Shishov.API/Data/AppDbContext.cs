using Microsoft.EntityFrameworkCore;
using WEB_253505_Shishov.Domain.Entities;

namespace WEB_253505_Shishov.API.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{

	}

	public DbSet<Category> Categories { get; set; }
	public DbSet<Constructor> Constructors { get; set; }
}
