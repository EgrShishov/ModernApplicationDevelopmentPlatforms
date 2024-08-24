using Microsoft.AspNetCore.Mvc;
using WEB_253505_Shishov.API.Services.CategoryService;

namespace WEB_253505_Shishov.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        return Ok(await _categoryService.GetCategoryListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int? id)
    {
		throw new NotImplementedException();
	}
    [HttpPost]
    public IActionResult Create()
    {
		throw new NotImplementedException();
	}

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Edit(int? id)
    {
		throw new NotImplementedException();
	}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int? id)
    {
        throw new NotImplementedException();
    }
}
