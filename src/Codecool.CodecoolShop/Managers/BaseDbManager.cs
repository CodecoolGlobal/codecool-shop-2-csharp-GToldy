using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Daos;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace Codecool.CodecoolShop.Managers
{
    public class BaseDbManager
    {
        private readonly IProductDbDao _productDbDao;
        private readonly IProductCategoryDbDao _productCategoryDbDao;
        private readonly ISupplierDbDao _sipplierDbDao;
        public string connectionString => ConfigurationManager.AppSetting["connectionString"];

        public BaseDbManager(IProductDbDao productDbDao, IProductCategoryDbDao productCategoryDbDao, ISupplierDbDao sipplierDbDao)
        {
            _productDbDao = productDbDao;
            _productCategoryDbDao = productCategoryDbDao;
            _sipplierDbDao = sipplierDbDao;
        }
    }
}
