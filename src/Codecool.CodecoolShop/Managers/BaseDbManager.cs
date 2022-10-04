using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Daos;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace Codecool.CodecoolShop.Managers
{
    public class BaseDbManager
    {
        private readonly IProductDbDao _productDbDao;
        private readonly IProductCategoryDbDao _productCategoryDbDao;
        private readonly ISupplierDbDao _sipplierDbDao;
        public string connectionString => ConfigurationManager.AppSettings["connectionString"];

        public BaseDbManager(IProductDbDao productDbDao, IProductCategoryDbDao productCategoryDbDao, ISupplierDbDao sipplierDbDao)
        {
            EnsureConnectionSuccessful();
            _productDbDao = productDbDao;
            _productCategoryDbDao = productCategoryDbDao;
            _sipplierDbDao = sipplierDbDao;
        }

        public void EnsureConnectionSuccessful() 
        {
            if (!TestConnection())
            {

            }
        }


        public bool TestConnection()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (System.Exception)
                {

                    return false;
                }
            }
        }
    }
}
