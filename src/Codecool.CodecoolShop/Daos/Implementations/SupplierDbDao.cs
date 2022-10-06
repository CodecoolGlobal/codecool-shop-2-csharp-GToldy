using Codecool.CodecoolShop.Models;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class SupplierDbDao : ISupplierDbDao
    {
        private readonly string _connectionString;
        private static SupplierDbDao instance = null;

        private SupplierDbDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static SupplierDbDao GetInstance(string connectionString)
        {
            if (instance is null)
            {
                instance = new SupplierDbDao(connectionString);
            }
            return instance;
        }

        public void Add(Supplier item)
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

        public Supplier Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Supplier> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
