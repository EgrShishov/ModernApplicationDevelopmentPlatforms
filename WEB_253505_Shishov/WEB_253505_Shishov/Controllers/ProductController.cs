﻿using Microsoft.AspNetCore.Mvc;

namespace WEB_253505_Shishov.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}