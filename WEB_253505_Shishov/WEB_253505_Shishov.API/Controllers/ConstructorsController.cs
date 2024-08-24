using Microsoft.AspNetCore.Mvc;
using WEB_253505_Shishov.API.Services.ConstructorService;

namespace WEB_253505_Shishov.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConstructorsController : ControllerBase
{
    private readonly IConstructorService _constructorService;

    public ConstructorsController(IConstructorService constructorService)
    {
        _constructorService = constructorService;
    }

    [HttpGet]
    [Route("{category}")]
    public async Task<IActionResult> GetConstructors(string? category, int pageNo = 1,int pageSize = 3)
    {
			return Ok(await _constructorService.GetProductListAsync(
                                        category,
                                        pageNo,
                                        pageSize));
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
