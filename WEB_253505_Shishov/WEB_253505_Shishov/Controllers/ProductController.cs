using Microsoft.AspNetCore.Mvc;
using WEB_253505_Shishov.Extensions;
using WEB_253505_Shishov.Services.CategoryService;
using WEB_253505_Shishov.Services.ConstructorService;

namespace WEB_253505_Shishov.Controllers
{
    [Route("Catalog")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IConstructorService _constructorService;
        public ProductController(IConstructorService constructorService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _constructorService = constructorService;
        }

        [HttpGet("{category?}")]
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

			var currentCategory = category == null ? "Все" : allCategories.Data!.FirstOrDefault(c => c.NormalizedName == category)?.Name;

			ViewData["Categories"] = allCategories.Data;
            ViewData["CurrentCategory"] = currentCategory;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PagerAndCardsPartial", new
                {
                    CurrentCategory = category,
                    Categories = allCategories.Data,
                    Products = productResponse.Data!.Items,
                    ReturnUrl = Request.Path + Request.QueryString.ToUriComponent(),
                    CurrentPage = productResponse.Data.CurrentPage,
                    TotalPages = productResponse.Data.TotalPages,
                    Admin = false
			    });
            }

			return View(productResponse.Data);
        }
    }
}
