using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Services.CategoryService;
using WEB_253505_Shishov.Services.ConstructorService;

namespace WEB_253505_Shishov.Areas.Admin.Pages;

[Authorize(Policy = "admin")]
public class DeleteModel : PageModel
{
	private readonly IConstructorService _constructorService;
	private readonly ICategoryService _categoryService;
	public DeleteModel(ICategoryService categoryService, IConstructorService constructorService)
	{
		_categoryService = categoryService;
		_constructorService = constructorService;
	}
	[BindProperty]
	public Constructor Constructor { get; set; } = default!;

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

		Constructor = response.Data!;

		return Page();
	}

	public async Task<IActionResult> OnPostAsync(int? id)
	{
		if (id == null)
		{
			return NotFound();
		}

		try
		{
			await _constructorService.DeleteProductAsync(id.Value);
		}
		catch (Exception e)
		{
			return NotFound(e.Message);
		}

		return RedirectToPage("./Index");
	}
}
