using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Extensions;
using WEB_253505_Shishov.Services.ConstructorService;

namespace WEB_253505_Shishov.Areas.Admin.Pages;

[Authorize(Policy = "admin")]
public class IndexModel : PageModel
{
	private readonly IConstructorService _constructorService;
	public IndexModel(IConstructorService constructorService)
	{
		_constructorService = constructorService;
	}
	public async Task<IActionResult> OnGetAsync(int pageNo = 1)
	{
		var response = await _constructorService.GetProductListAsync(null, pageNo);
		if (response.Successfull)
		{
			Constructors = response.Data.Items;
			TotalPages = response.Data.TotalPages;
			CurrentPage = pageNo;

			if (Request.IsAjaxRequest())
			{
				return Partial("_PagerAndCardsPartial", new
				{
					Admin = true,
					CurrentPage = CurrentPage,
					TotalPages = TotalPages,
					Constructors = Constructors
				});
			}

			return Page();
		}

		return RedirectToPage("/Error");
	}

	[BindProperty]
	public List<Constructor> Constructors { get; set; } = new();

	public int CurrentPage { get; set; }
	public int TotalPages { get; set; }
}
