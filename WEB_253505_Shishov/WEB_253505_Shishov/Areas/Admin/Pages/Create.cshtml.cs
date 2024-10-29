using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Services.CategoryService;
using WEB_253505_Shishov.Services.ConstructorService;

namespace WEB_253505_Shishov.Areas.Admin.Pages;

[Authorize(Policy = "admin")]
public class CreateModel : PageModel
{
    private readonly IConstructorService _constructorService;
	private readonly ICategoryService _categoryService;

	public CreateModel(IConstructorService constructorService, ICategoryService categoryService)
    {
        _constructorService = constructorService;
		_categoryService = categoryService;

		Categories = new SelectList(_categoryService.GetCategoryListAsync().Result.Data, "Id", "Name");
    }
    public async Task<IActionResult> OnGetAsync()
    {
		return Page();
	}

	[BindProperty]
	public IFormFile? Image { get; set; }

	[BindProperty]
	public Constructor Constructor { get; set; } = default!;

	public SelectList Categories { get; set; } 
	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		var response = await _constructorService.CreateProductAsync(Constructor, Image);

		if (!response.Successfull)
		{
			return Page();
		}

		return RedirectToPage("./Index");
	}
}
