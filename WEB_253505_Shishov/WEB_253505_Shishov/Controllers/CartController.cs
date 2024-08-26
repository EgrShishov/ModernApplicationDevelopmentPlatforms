using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Services.ConstructorService;

namespace WEB_253505_Shishov.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IConstructorService _constructorService;
        private readonly Cart _cart;
        public CartController(IConstructorService constructorService, Cart cart)
        {
            _constructorService = constructorService;
            _cart = cart;
        }

        public IActionResult Index()
        {
            return View(_cart);
        }

		[Route("[controller]/add/{id:int}")]
		public async Task<IActionResult> AddItem(int id, string returnUrl)
        {
            var data = await _constructorService.GetProductByIdAsync(id);
            if (data.Successfull)
            {
                _cart.AddToCart(data.Data);
            }

            return Redirect(returnUrl);
        }

        [Route("[controller]/remove/{id:int}")]
        public async Task<IActionResult> RemoveItem(int id, string returnUrl)
        {
            var data = await _constructorService.GetProductByIdAsync(id);
            if (data.Successfull)
            {
                _cart.RemoveItems(id);
            }

            return Redirect(returnUrl);
        }
    }
}
