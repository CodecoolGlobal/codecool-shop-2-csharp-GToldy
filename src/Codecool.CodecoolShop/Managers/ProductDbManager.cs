using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using System.Collections;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Managers
{
    public class ProductDbManager : BaseDbManager
    {
        private readonly IProductDbDao _productDbDao;
        private readonly IProductCategoryDbDao _productCategoryDbDao;
        private readonly ISupplierDbDao _supplierDbDao;

        public ProductDbManager(IProductDbDao productDbDao, IProductCategoryDbDao productCategoryDbDao, ISupplierDbDao supplierDbDao)
        {
            _productDbDao = productDbDao;
            _productCategoryDbDao = productCategoryDbDao;
            _supplierDbDao = supplierDbDao;
        }

        public Product GetProductById(int productId)
        {
            return _productDbDao.Get(productId);
        }

        public Supplier GetSupplierById(int supplierId)
        {
            return _supplierDbDao.Get(supplierId);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productDbDao.GetAll();
        }
    }
}
