using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Services.CategoryService;
using WEB_253505_Shishov.Services.ConstructorService;

namespace WEB_253505_Shishov.Areas.Admin.Pages;

public class IndexModel : PageModel
{
	private readonly IConstructorService _constructorService;
	public IndexModel(IConstructorService constructorService)
	{
		_constructorService = constructorService;

		var constructors = _constructorService.GetProductListAsync(null).Result.Data;
		for (int i = 0; i < constructors.TotalPages; i++)
		{
			Constructors.AddRange(_constructorService.GetProductListAsync(null, i).Result.Data.Items);
		}
	}
	public void OnGet()
	{

	}

	[BindProperty]
	public List<Constructor> Constructors { get; set; } = new();
}
