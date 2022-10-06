using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductCategoryDbDao : IProductCategoryDbDao
    {
        private readonly string _connectionString;
        private static ProductCategoryDbDao instance = null;

        private ProductCategoryDbDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static ProductCategoryDbDao GetInstance(string connectionString)
        {
            if (instance is null)
            {
                instance = new ProductCategoryDbDao(connectionString);
            }
            return instance;
        }

        public void Add(ProductCategory item)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public ProductCategory Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
