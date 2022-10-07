using Azure.Core;
using Codecool.CodecoolShop.Models;
using Microsoft.AspNetCore.Http;
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
            const string query = @"SELECT Product.id, Product.name, Product.description, Product.players, Product.currency, Product.default_price, ProductCategory.name AS category, Supplier.name AS supplier, Product.image FROM Product
                                    LEFT JOIN ProductCategory ON Product.product_category_id = ProductCategory.id
                                    LEFT JOIN Supplier ON Product.supplier_id = Supplier.id
                                    WHERE Product.id = @id";
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
                    var category = reader.GetString("category");
                    ProductCategory productCategory = new ProductCategory() { Name = category };
                    var supplierAsString = reader.GetString("supplier");
                    Supplier productSupplier = new Supplier() { Name = supplierAsString };
                    var price = reader.GetDecimal("default_price");
                    var player = reader["players"] as string;
                    var currency = reader["currency"] as string;
                    var image = reader["image"] as string;
                    var product = new Product() { Id = (int)reader["id"], Name = name, Description = description, ProductCategory = productCategory, Supplier = productSupplier, DefaultPrice = price, Players = player, Currency = currency, Image = image };
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
            const string query = @"SELECT Product.id, Product.name, Product.description, Product.players, Product.currency, Product.default_price, ProductCategory.name AS category, Supplier.name AS supplier, Product.image FROM Product
                                    LEFT JOIN ProductCategory ON Product.product_category_id = ProductCategory.id
                                    LEFT JOIN Supplier ON Product.supplier_id = Supplier.id";
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
                        var name = reader.GetString("name");
                        var description = reader.GetString("description");
                        var category = reader.GetString("category");
                        ProductCategory productCategory = new ProductCategory() { Name = category };
                        var supplier = reader.GetString("supplier");
                        Supplier productSupplier = new Supplier() { Name = supplier };
                        var price = reader.GetDecimal("default_price");
                        var player = reader["players"] as string;
                        var currency = reader["currency"] as string;
                        var image = reader["image"] as string;
                        var product = new Product() { Id = (int)reader["id"], Name = name, Description = description, ProductCategory = productCategory, Supplier = productSupplier, DefaultPrice = price, Players = player, Currency = currency, Image = image };
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

        public IEnumerable<Product> GetBySupplier(Supplier supplier)
        {
            var supplierId = supplier.Id;
            const string query = @"SELECT Product.id, Product.name, Product.description, Product.players, Product.currency, Product.default_price, ProductCategory.name AS category, Supplier.name AS supplier, Product.image, Product.supplier_id FROM Product
                                    LEFT JOIN ProductCategory ON Product.product_category_id = ProductCategory.id
                                    LEFT JOIN Supplier ON Product.supplier_id = Supplier.id
                                    WHERE supplier_id = @supplierId";
            try
            {
                var results = new List<Product>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@supplierId", supplier.Id);
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return results;

                    while (reader.Read())
                    {
                        var name = reader.GetString("name");
                        var description = reader.GetString("description");
                        var category = reader.GetString("category");
                        ProductCategory productCategory = new ProductCategory() { Name = category };
                        var supplierAsString = reader.GetString("supplier");
                        Supplier productSupplier = new Supplier() { Name = supplierAsString };
                        var price = reader.GetDecimal("default_price");
                        var player = reader["players"] as string;
                        var currency = reader["currency"] as string;
                        var image = reader["image"] as string;
                        var product = new Product() { Id = (int)reader["id"], Name = name, Description = description, ProductCategory = productCategory, Supplier = productSupplier, DefaultPrice = price, Players = player, Currency = currency, Image = image };
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

        public IEnumerable<Product> GetByProductCategory(ProductCategory productCategory)
        {
            int id = productCategory.Id;
            const string query = @"SELECT Product.id, Product.name, Product.description, Product.players, Product.currency, Product.default_price, ProductCategory.name AS category, Supplier.name AS supplier, Product.image FROM Product
                                    LEFT JOIN ProductCategory ON Product.product_category_id = ProductCategory.id
                                    LEFT JOIN Supplier ON Product.supplier_id = Supplier.id
                                    WHERE Product.product_category_id = @id";
            try
            {
                var results = new List<Product>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                        return results;

                    while (reader.Read())
                    {
                        var name = reader.GetString("name");
                        var description = reader.GetString("description");
                        var category = reader.GetString("category");
                        ProductCategory newProductCategory = new ProductCategory() { Name = category };
                        var supplierAsString = reader.GetString("supplier");
                        Supplier productSupplier = new Supplier() { Name = supplierAsString };
                        var price = reader.GetDecimal("default_price");
                        var player = reader["players"] as string;
                        var currency = reader["currency"] as string;
                        var image = reader["image"] as string;
                        var product = new Product() { Id = (int)reader["id"], Name = name, Description = description, ProductCategory = newProductCategory, Supplier = productSupplier, DefaultPrice = price, Players = player, Currency = currency, Image = image };
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

        public IEnumerable<Product> GetByBoth(Supplier supplier, ProductCategory productCategory)
        {
            int supplierId = supplier.Id;
            int categoryId = productCategory.Id;
            const string query = @"SELECT * FROM Product WHERE @product_category_id = categoryId AND @supplier_id = supplierId";
            try
            {
                var results = new List<Product>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    var cmd = new SqlCommand(query, connection);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    cmd.Parameters.AddWithValue("@product_category_id", categoryId);
                    cmd.Parameters.AddWithValue("@supplier_id", supplierId);
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

        public void Update(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
