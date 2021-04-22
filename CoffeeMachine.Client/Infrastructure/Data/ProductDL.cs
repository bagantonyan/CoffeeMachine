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

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            Product product = null;
                            while (reader.Read())
                            {
                                product = new Product();
                                product.Id = reader.GetInt32(0);
                                product.Name = reader.GetString(1);
                                product.Water = reader.GetDouble(2);
                                product.Sugar = reader.GetDouble(3);
                                product.Coffee = reader.GetDouble(4);
                                product.Price = reader.GetDecimal(5);
                                products.Add(product);
                            }
                        }

                        reader.Close();
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

                        var reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            product.Id = reader.GetInt32(0);
                            product.Name = reader.GetString(1);
                            product.Water = reader.GetDouble(2);
                            product.Sugar = reader.GetDouble(3);
                            product.Coffee = reader.GetDouble(4);
                            product.Price = reader.GetDecimal(5);
                        }

                        reader.Close();
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
