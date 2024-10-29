using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Services.CategoryService;
using WEB_253505_Shishov.Services.ConstructorService;

namespace WEB_253505_Shishov.Areas.Admin.Pages;

[Authorize(Policy = "admin")]
public class EditModel : PageModel
{
	private readonly IConstructorService _constructorService;
	private readonly ICategoryService _categoryService;
	public EditModel(IConstructorService constructorService, ICategoryService categoryService)
	{
		_constructorService = constructorService;
		_categoryService = categoryService;

		Categories = new SelectList(_categoryService.GetCategoryListAsync().Result.Data, "Id", "Name");
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

		CurrentImage = Constructor.Image;

		return Page();
	}

	[BindProperty]
	public IFormFile? Image { get; set; }	

	[BindProperty]
	public string? CurrentImage { get; set; }

	[BindProperty]
	public Constructor Constructor { get; set; } = default!;
	public SelectList Categories { get; set; }

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		try
		{
			await _constructorService.UpdateProductAsync(Constructor.Id, Constructor, Image);
		}
		catch (Exception)
		{
			if (!await ConstructorsExists(Constructor.Id))
			{
				return NotFound();
			}
			else
			{
				throw;
			}
		}

		return RedirectToPage("./Index");
	}

	private async Task<bool> ConstructorsExists(int id)
	{
		var response = await _constructorService.GetProductByIdAsync(id);
		return response.Successfull;
	}
}
