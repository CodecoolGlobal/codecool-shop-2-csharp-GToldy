using System.Collections.Generic;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao productDao;
        private readonly IProductCategoryDao productCategoryDao;
        private readonly ISupplierDao supplierDao;

        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao, ISupplierDao supplierDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
            this.supplierDao = supplierDao;
        }

        public ProductCategory GetProductCategoryById(int categoryId)
        {
            return this.productCategoryDao.Get(categoryId);
        }

        public IEnumerable<ProductCategory> GetAllProductCategory()
        {
            return this.productCategoryDao.GetAll();
        }

        public IEnumerable<Product> GetAllProductsForCategory(int categoryId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            return this.productDao.GetBy(category);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return this.productDao.GetAll();
        }

        public Supplier GetSupplierById(int id)
        {
            return this.supplierDao.Get(id);
        }

        public IEnumerable<Supplier> GetAllSupplier()
        {
            return this.supplierDao.GetAll();
        }

        public IEnumerable<Product> GetProductsForSupplier(int supplierId)
        {
            Supplier supplier = this.supplierDao.Get(supplierId);
            return this.productDao.GetBy(supplier);
        }

        public Product GetProductById(int productId)
        {
            return this.productDao.Get(productId);
        }
    }
}
