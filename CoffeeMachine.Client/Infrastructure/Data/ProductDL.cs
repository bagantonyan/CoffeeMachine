using CoffeeMachine.Client.Infrastructure.Data.Interfaces;
using CoffeeMachine.Client.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace CoffeeMachine.Client.Infrastructure.Data
{
    public class ProductDL : IProductDL
    {
        private readonly string _connectionString;

        public ProductDL()
        {
            _connectionString = Startup.GetDbConnection();
        }

        public void UpdateTest()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Product SET Water = @Water, Sugar = @Sugar WHERE Id = 1";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.Add("@Water", DbType.Double).Value = 0.7;
                        command.Parameters.Add("@Sugar", DbType.Double).Value = 0.7;

                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = new List<Product>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    string query = "SELECT * FROM Product";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        connection.Open();

                        var result = command.ExecuteReader();

                        if (result.HasRows)
                        {
                            Product product = null;
                            while (result.Read())
                            {
                                product = new Product();
                                product.Id = result.GetInt32(0);
                                product.Name = result.GetString(1);
                                product.Water = result.GetDouble(2);
                                product.Sugar = result.GetDouble(3);
                                product.Coffee = result.GetDouble(4);
                                product.Price = result.GetDecimal(5);
                                products.Add(product);
                            }
                        }

                        connection.Close();
                    }
                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
            }
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = new Product();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    string query = "SELECT * FROM Product WHERE Id = @Id";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.Add("@Id", DbType.Int32).Value = id;

                        connection.Open();

                        var result = command.ExecuteReader();

                        if (result.Read())
                        {
                            product.Id = result.GetInt32(0);
                            product.Name = result.GetString(1);
                            product.Water = result.GetDouble(2);
                            product.Sugar = result.GetDouble(3);
                            product.Coffee = result.GetDouble(4);
                            product.Price = result.GetDecimal(5);
                        }

                        connection.Close();
                    }
                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
            }
            return product;
        }
    }
}
