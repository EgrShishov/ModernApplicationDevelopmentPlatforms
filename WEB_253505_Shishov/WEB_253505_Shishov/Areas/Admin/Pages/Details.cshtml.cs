using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Services.CategoryService;
using WEB_253505_Shishov.Services.ConstructorService;

namespace WEB_253505_Shishov.Areas.Admin.Pages;

[Authorize(Policy = "admin")]
public class DetailsModel : PageModel
{
	private readonly IConstructorService _constructorService;
	private readonly ICategoryService _categoryService;
	public DetailsModel(ICategoryService categoryService, IConstructorService constructorService)
	{
		_categoryService = categoryService;
		_constructorService = constructorService;
	}
	public async Task<IActionResult> OnGetAsync(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		var response = await _constructorService.GetProductByIdAsync(id.Value);

		if (!response.Successfull)
		{
			return NotFound();
		}

		var categoryResponse = await _categoryService.GetCategoryListAsync();
		if (!categoryResponse.Successfull)
		{
			return NotFound();
		}

		Constructor = response.Data!;
		CategoryName = categoryResponse.Data.FirstOrDefault(c => c.Id == Constructor.CategoryId).Name;

		return Page();
	}

	[BindProperty]
	public Constructor Constructor { get; set; } = default;

	[BindProperty]
	public string CategoryName { get; set; } = string.Empty;
}
