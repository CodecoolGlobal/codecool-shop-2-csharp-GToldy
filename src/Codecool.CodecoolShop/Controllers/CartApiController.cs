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

        [HttpGet]
        public List<Item> GetAll()
        {
            if (GetCartFromSession() is null)
            {
                SetCartToSession(_cart);
            }
            return new List<Item>(_cart.Items);
        }

        //Route: /api/cart/Add/1
        [HttpGet("{id}")]
        public List<Item> Add(int id)
        {
            var product = _productService.GetProductById(id);

            if (IsProductInCart(product))
            {
                var item = FindItemInCart(product);
                item.Quantity++;
            }
            else
            {
                _cart.Items.Add(new Item { Product = product, Quantity = 1 });
            }

            return new List<Item>(_cart.Items);
        }

        //Route: /api/cart/Delete/1
        [HttpGet("{id}")]
        public List<Item> Delete(int id)
        {
            var product = _productService.GetProductById(id);
            var item = FindItemInCart(product);

            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                _cart.Items.Remove(item);
            }

            return new List<Item>(_cart.Items);
        }

        private bool IsProductInCart(Product product)
        {
            var item = _cart.Items.Where(item => item.Product.Equals(product));
            return item.Any();
        }

        private Item FindItemInCart(Product product)
        {
            var item = _cart.Items.Find(item => item.Product.Equals(product));
            return item;
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