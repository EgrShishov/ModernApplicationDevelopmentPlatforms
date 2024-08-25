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
    [Route("{category?}")]
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
        var data = await _constructorService.GetProductByIdAsync(id.Value);

        if (!data.Successfull)
        {
            return Problem(data.ErrorMessage);
        }

        return Ok(data.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Domain.Entities.Constructor constructor)
    {
        var response = await _constructorService.CreateProductAsync(constructor);

        if (!response.Successfull)
        {
            return Problem(response.ErrorMessage);
        }

        return Ok(response.Data);
	}

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Edit(int? id)
    {
			throw new NotImplementedException();
	}

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int? id)
    {
        await _constructorService.DeleteProductAsync(id.Value);
        return NoContent();
	}
}
