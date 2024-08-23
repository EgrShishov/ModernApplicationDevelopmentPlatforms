using Microsoft.AspNetCore.Mvc;

namespace WEB_253505_Shishov.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Logout()
        {
            return NoContent();
        }
    }
}
