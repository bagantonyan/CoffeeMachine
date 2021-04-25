using CoffeeMachine.Client.Infrastructure.Data.Interfaces;
using CoffeeMachine.Client.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace CoffeeMachine.Client.Infrastructure.Data
{
    public class StoreDL : IStoreDL
    {
        private readonly string _connectionString;

        public StoreDL()
        {
            _connectionString = Startup.GetDbConnection();
        }

        public Store GetStore()
        {
            var store = new Store();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    string query = @"SELECT * FROM Store
                                     WHERE Id = 1";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            store.Water = reader.GetDouble(1);
                            store.Sugar = reader.GetDouble(2);
                            store.Coffees = reader.GetDouble(3);
                        }

                        reader.Close();
                    }
                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
            }

            return store;
        }

        public void TakeIngridients(Coffee product)
        {
            var store = GetStore();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Store SET Water = @Water, Sugar = @Sugar, Coffees = @Coffees WHERE Id = 1";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        connection.Open();

                        double water = store.Water - product.Water;
                        double sugar = store.Sugar - product.Sugar;
                        double coffee = store.Coffees - product.Coffees;

                        command.Parameters.Add("@Water", DbType.Double).Value = water;
                        command.Parameters.Add("@Sugar", DbType.Double).Value = sugar;
                        command.Parameters.Add("@Coffees", DbType.Double).Value = coffee;

                        command.ExecuteNonQuery();
                    }
                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
            }
        }

        public void RechargeStore()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Store SET Water = 1000, Sugar = 300, Coffees = 500 WHERE Id = 1";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
            }
        }
    }
}
