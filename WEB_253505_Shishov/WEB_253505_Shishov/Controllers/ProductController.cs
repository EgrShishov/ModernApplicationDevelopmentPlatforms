using Microsoft.AspNetCore.Mvc;
using WEB_253505_Shishov.Services.CategoryService;
using WEB_253505_Shishov.Services.ConstructorService;

namespace WEB_253505_Shishov.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IConstructorService _constructorService;
        public ProductController(IConstructorService constructorService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _constructorService = constructorService;
        }
        public async Task<IActionResult> Index(string? category, int pageNo = 1)
        {
            var productResponse =
                await _constructorService.GetProductListAsync(category, pageNo);
            if (!productResponse.Successfull)
            {
                return NotFound(productResponse.ErrorMessage);
            }

            var allCategories =
                await _categoryService.GetCategoryListAsync();
            if (!allCategories.Successfull)
            {
                return NotFound(allCategories.ErrorMessage);
            }

            var currentCategory = category == null ? "Все" : allCategories.Data.FirstOrDefault(c => c.NormalizedName.Equals(category)).Name;
			ViewData["Categories"] = allCategories.Data;
            ViewData["CurrentCategory"] = currentCategory;

			return View(productResponse.Data);
        }
    }
}
