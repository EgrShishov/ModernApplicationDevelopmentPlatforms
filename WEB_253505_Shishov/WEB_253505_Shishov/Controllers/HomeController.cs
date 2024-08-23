using Microsoft.AspNetCore.Mvc;
using WEB_253505_Shishov.Helpers;
using WEB_253505_Shishov.Models;

namespace WEB_253505_Shishov.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Message"] = "Лабораторная работа 2";

            var demoList = new List<ListDemo>
            {
                new ListDemo {Id = 1, Name = "Item 1"},
                new ListDemo {Id = 2, Name = "Item 2"},
                new ListDemo {Id = 3, Name = "Item 3"},
            };

            var viewModel = new IndexViewModel
            {
                ListItems = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(demoList, "Id", "Name")
            };

            return View(viewModel);
        }
    }
}
