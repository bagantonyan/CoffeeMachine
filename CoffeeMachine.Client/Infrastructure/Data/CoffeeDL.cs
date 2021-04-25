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
    public class CoffeeDL : ICoffeeDL
    {
        private readonly string _connectionString;

        public CoffeeDL()
        {
            _connectionString = Startup.GetDbConnection();
        }

        public IEnumerable<Coffee> GetAllCoffees()
        {
            var coffees = new List<Coffee>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    string query = "SELECT * FROM Coffee";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            Coffee coffee = null;
                            while (reader.Read())
                            {
                                coffee = new Coffee();
                                coffee.Id = reader.GetInt32(0);
                                coffee.Name = reader.GetString(1);
                                coffee.Water = reader.GetDouble(2);
                                coffee.Sugar = reader.GetDouble(3);
                                coffee.Coffees = reader.GetDouble(4);
                                coffee.Price = reader.GetDecimal(5);
                                coffees.Add(coffee);
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
            return coffees;
        }

        public Coffee GetCoffeeById(int id)
        {
            var coffee = new Coffee();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    string query = "SELECT * FROM Coffee WHERE Id = @Id";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.Add("@Id", DbType.Int32).Value = id;

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            coffee.Id = reader.GetInt32(0);
                            coffee.Name = reader.GetString(1);
                            coffee.Water = reader.GetDouble(2);
                            coffee.Sugar = reader.GetDouble(3);
                            coffee.Coffees = reader.GetDouble(4);
                            coffee.Price = reader.GetDecimal(5);
                        }

                        reader.Close();
                    }
                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
            }
            return coffee;
        }
    }
}
