using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Services.ConstructorService;

namespace WEB_253505_Shishov.Areas.Admin.Pages;

public class EditModel : PageModel
{
	private readonly IConstructorService _constructorService;
	public EditModel(IConstructorService constructorService)
	{
		_constructorService = constructorService;
	}
	public void OnGet()
	{

	}

	[BindProperty]
	public IFormFile? Image { get; set; }	

	[BindProperty]
	public IFormFile? CurrentImage { get; set; }

	[BindProperty]
	public Constructor Constructor { get; set; } = default!;

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
