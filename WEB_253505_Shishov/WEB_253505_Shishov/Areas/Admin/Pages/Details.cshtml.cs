using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253505_Shishov.Services.CategoryService;
using WEB_253505_Shishov.Services.ConstructorService;

namespace WEB_253505_Shishov.Areas.Admin.Pages;

public class DetailsModel : PageModel
{
	private readonly IConstructorService _constructorService;
	private readonly ICategoryService _categoryService;
	public DetailsModel(ICategoryService categoryService, IConstructorService constructorService)
	{
		_categoryService = categoryService;
		_constructorService = constructorService;
	}
	public void OnGet()
	{
	}
}
