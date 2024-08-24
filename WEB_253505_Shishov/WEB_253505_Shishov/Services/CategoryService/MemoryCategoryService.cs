using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Domain.Models;

namespace WEB_253505_Shishov.Services.CategoryService;

public class MemoryCategoryService : ICategoryService
{
	public Task<ResponseData<List<Category>>> GetCategoryListAsync()
	{
		List<Category> legoCategories = new List<Category>
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

		var result = ResponseData<List<Category>>.Success(legoCategories);

		return Task.FromResult(result);
	}
}
