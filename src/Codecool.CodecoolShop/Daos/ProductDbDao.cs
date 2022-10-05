using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace Codecool.CodecoolShop.Daos
{
    public class ProductDbDao : IProductDbDao
    {
        private readonly string _connectionString;
        private readonly ISupplierDbDao _supplierDbDao;
        private readonly IProductDbDao _productDbDao;

        public ProductDbDao(string connectionString, ISupplierDbDao supplierDbDao, IProductDbDao productDbDao)
        {
            _connectionString = connectionString;
            _supplierDbDao = supplierDbDao;
            _productDbDao = productDbDao;
        }

        public void AddToDb(Product product)
        {
            const string insertStatement = @"INSERT INTO Products (name) VALUES (@name) SELECT SCOPE_IDENTITY();";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var sqlCommand = new SqlCommand(insertStatement, connection);
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    sqlCommand.Parameters.AddWithValue("@name", product.Name);
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }
    }
}
