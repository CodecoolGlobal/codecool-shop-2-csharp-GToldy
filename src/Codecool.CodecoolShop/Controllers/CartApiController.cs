using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Newtonsoft.Json;

namespace Codecool.CodecoolShop.Controllers
{
    [ApiController]
    [Route("/api/cart/[action]")]
    public class CartApiController : Controller
    {
        private readonly ProductService _productService;
        private Cart _cart;

        public CartApiController(ProductService productService, Cart cart)
        {
            _productService = productService;
            _cart = cart;
        }

        private Cart GetCartFromSession()
        {
            return HttpContext.Session.GetObjectFromJson<Cart>("cart");
        }

        private void SetCartToSession(Cart cart)
        {
            HttpContext.Session.SetObjectAsJson("cart", cart);
        }
    }
}