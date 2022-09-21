using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoryApiController : ControllerBase
    {
        private readonly ProductService productService;

        public CategoryApiController(ProductService productService)
        {
            this.productService = productService;
        }

        

        [HttpGet]
        public List<Product> GetProducts(int id)
        {
            var response = productService.GetProductsForCategory(id);
            
            return new List<Product>(response) ;
        }

        [HttpGet]
        public List<ProductCategory> GetProductCategories()
        {
            var response = productService.GetProductCategory();

            return new List<ProductCategory>(response);
        }
    }
}
