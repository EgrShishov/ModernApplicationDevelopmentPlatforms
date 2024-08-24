using Microsoft.EntityFrameworkCore;
using WEB_253505_Shishov.API.Data;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Domain.Models;

namespace WEB_253505_Shishov.API.Services.CategoryService;

public class CategoryService : ICategoryService
{
	private readonly AppDbContext _context;
	public CategoryService(AppDbContext context)
	{
		_context = context;
	}

	public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
	{
		var categories = await _context.Categories.ToListAsync();
		if (!categories.Any() || categories is null)
		{
			return ResponseData<List<Category>>.Error("No categories in db");
		}

		return ResponseData<List<Category>>.Success(categories);
	}
}
