using Codecool.CodecoolShop.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class ProductDbDao : IProductDbDao
    {
        private readonly string _connectionString;
        private static ProductDbDao instance = null;

        private ProductDbDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static ProductDbDao GetInstance(string connectionString)
        {
            if (instance is null)
            {
                instance = new ProductDbDao(connectionString);
            }
            return instance;
        }

        public void Add(Product product)
        {
            const string query = @"INSERT INTO Product (name) VALUES (@name) SELECT SCOPE_IDENTITY();";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                    cmd.Parameters.AddWithValue("@name", product.Name);

                    product.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Product Get(int id)
        {
            const string query = @"SELECT * FROM Product WHERE id = @id";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        return null;
                    }
                    var name = reader.GetString("name");
                    var description = reader.GetString("description");

                    var product = new Product() { Id = id, Name = name, Description = description };
                    connection.Close();
                    return product;
                }
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public IEnumerable<Product> GetAll()
        {
            const string query = @"SELECT * FROM Product";
            try
            {
                var results = new List<Product>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return results;

                    while (reader.Read())
                    {
                        var name = reader["name"] as string;
                        var description = reader["description"] as string;
                        var image = reader["image"] as string;
                        var product = new Product { Id = (int)reader["id"], Name = name, Description = description, Image = image };
                        results.Add(product);
                    }
                    connection.Close();
                }

                return results;
            }
            catch (SqlException e)
            {
                throw new RuntimeWrappedException(e);
            }
        }

        public IEnumerable<Product> GetBy(Supplier supplier)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetBy(ProductCategory productCategory)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> GetBy(Supplier supplier, ProductCategory productCategory)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
