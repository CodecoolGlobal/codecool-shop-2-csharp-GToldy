using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryApiController : ControllerBase
    {
        private readonly ProductService productService;

        public CategoryApiController(ProductService productService)
        {
            this.productService = productService;
        }

        

        [HttpGet]
        public List<Product> Index(int id)
        {
            var response = productService.GetProductsForCategory(id);
            
            return new List<Product>(response) ;
        }
    }
}
