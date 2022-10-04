using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SupplierApiController : ControllerBase
    {
        private readonly ProductService productService;

        public SupplierApiController(ProductService productService)
        {

            this.productService = productService;
        }



        [HttpGet]
        public List<Product> GetProductBySupplier(int id)
        {
            var response = productService.GetProductsForSupplier(id);

            return new List<Product>(response);
        }

        [HttpGet]
        public List<Product> GetAllProducts()
        {
            var response = productService.GetAllProducts();

            return new List<Product>(response);
        }

        [HttpGet]
        public List<Supplier> GetProductSuppliers()
        {
            var response = productService.GetAllSupplier();

            return new List<Supplier>(response);
        }
    }
}
