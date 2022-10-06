using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Specialized;

namespace Codecool.CodecoolShop.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login(User user)
        {
            return View();
        }

        public IActionResult Register(User user)
        {
            return View();
        }
    }
}
