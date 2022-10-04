using System;
using System.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace Codecool.CodecoolShop
{
    public class BookDbManager
    {
        public string ConnectionString => ConfigurationManager.["connectionString"];
        public bool TestConnection()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}