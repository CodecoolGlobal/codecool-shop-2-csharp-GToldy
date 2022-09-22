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
        public List<Product> GetSupplier(int id)
        {
            var response = productService.GetProductsForSupplier(id);

            return new List<Product>(response);
        }

        [HttpGet]
        public List<Product> GetAllProductsSupplier()
        {
            var response = productService.GetProductsForSupplier();

            return new List<Product>(response);
        }

        [HttpGet]
        public List<Supplier> GetProductSuppliers()
        {
            var response = productService.GetProductSupplier();

            return new List<Supplier>(response);
        }
    }
}
