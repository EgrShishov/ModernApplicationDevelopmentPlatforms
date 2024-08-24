using WEB_253505_Shishov.Domain.Entities;

namespace WEB_253505_Shishov.API.Data;

public static class DbInitializer
{
	public static async Task SeedData(WebApplication app)
	{
		var baseUrl = app.Configuration.GetSection("BaseUrl").Value;

		List<Category> _categories = new List<Category>
		{
			new Category { Name = "City", NormalizedName = "city" },
			new Category { Name = "Star Wars", NormalizedName = "star-wars" },
			new Category { Name = "Technic", NormalizedName = "technic" },
			new Category { Name = "Ninjago", NormalizedName = "ninjago" },
			new Category { Name = "Friends", NormalizedName = "friends" },
			new Category { Name = "Harry Potter", NormalizedName = "harry-potter" },
			new Category { Name = "Creator", NormalizedName = "creator" },
			new Category { Name = "Marvel Super Heroes", NormalizedName = "marvel-super-heroes" },
			new Category { Name = "Architecture", NormalizedName = "architecture" },
			new Category { Name = "Disney", NormalizedName = "disney" }
		};

		List<Constructor> Constructors = new List<Constructor>
		{
			new Constructor
			{
				Picies = 100,
				Category = _categories[0],
				CategoryId = 1,
				Description = "City Police Patrol Car",
				Image = $"{baseUrl}/images/lego1.jpg",
				Price = 9.99m
			},
			new Constructor
			{
				Picies = 230,
				Category = _categories[1],
				CategoryId = 2,
				Description = "Star Wars X-Wing Starfighter",
				Image = $"{baseUrl}/images/lego2.jpg",
				Price = 29.99m
			},
			new Constructor
			{
				Picies = 1500,
				Category = _categories[6],
				CategoryId = 7,
				Description = "Technic Bugatti Chiron",
				Image = $"{baseUrl}/images/lego3.jpg",
				Price = 349.99m
			},
			new Constructor
			{
				Picies = 450,
				Category = _categories[6],
				CategoryId = 7,
				Description = "Ninjago Spinjitzu Masters",
				Image = $"{baseUrl}/images/lego4.jpg",
				Price = 49.99m
			},
			new Constructor
			{
				Picies = 500,
				Category = _categories[6],
				CategoryId = 7,
				Description = "Friends Heartlake City Resort",
				Image = $"{baseUrl}/images/lego5.jpg",
				Price = 59.99m
			},
			new Constructor
			{
				Picies = 750,
				Category = _categories[6],
				CategoryId = 7,
				Description = "Harry Potter Hogwarts Castle",
				Image = $"{baseUrl}/images/lego6.jpg",
				Price = 399.99m
			},
			new Constructor
			{
				Picies = 100,
				Category = _categories[6],
				CategoryId = 7,
				Description = "Creator 3-in-1 Rocket Truck",
				Image = $"{baseUrl}/images/lego7.jpeg",
				Price = 14.99m
			},
			new Constructor
			{
				Picies = 1200,
				Category = _categories[7],
				CategoryId = 8,
				Description = "Marvel Super Heroes Avengers Tower",
				Image = $"{baseUrl}/images/lego8.jpg",
				Price = 99.99m
			},
			new Constructor
			{
				Picies = 850,
				Category = _categories[8],
				CategoryId = 9,
				Description = "Architecture Empire State Building",
				Image = $"{baseUrl}/images/lego9_.jpg",
				Price = 129.99m
			},
			new Constructor
			{
				Picies = 400,
				Category = _categories[9],
				CategoryId = 10,
				Description = "Disney Frozen Ice Castle",
				Image = $"{baseUrl}/images/lego10.jpg",
				Price = 59.99m
			}
		};

		using var scope = app.Services.CreateScope();

		var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

		await context.Categories.AddRangeAsync(_categories);
		await context.Constructors.AddRangeAsync(Constructors);

		await context.SaveChangesAsync();
	}
}
