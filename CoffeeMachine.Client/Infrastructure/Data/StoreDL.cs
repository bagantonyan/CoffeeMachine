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

        public void TakeIngridients(Store store, Product product)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    string query = @"UPDATE Store SET Water = @Water, Sugar = @Sugar, Coffee = @Coffee WHERE Id = 1";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        connection.Open();

                        double water = store.Water - product.Water;
                        double sugar = store.Sugar - product.Sugar;
                        double coffee = store.Coffee - product.Coffee;

                        command.Parameters.Add("@Water", DbType.Double).Value = water;
                        command.Parameters.Add("@Sugar", DbType.Double).Value = sugar;
                        command.Parameters.Add("@Coffee", DbType.Double).Value = coffee;

                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
            }
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

                        var result = command.ExecuteReader();

                        if (result.Read())
                        {
                            store.Water = result.GetDouble(1);
                            store.Sugar = result.GetDouble(2);
                            store.Coffee = result.GetDouble(3);
                        }

                        connection.Close();
                    }
                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
            }

            return store;
        }

        public void UpdateTest()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                try
                {
                    //var store = GetStore();

                    string query = @"UPDATE Store SET Water = @Water, Sugar = @Sugar, Coffee = @Coffee WHERE Id = 1";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        //double water = store.Water - product.Water;
                        //double sugar = store.Sugar - product.Sugar;
                        //double coffee = store.Coffee - product.Coffee;

                        command.Parameters.Add("@Water", DbType.Double).Value = 0.8;
                        command.Parameters.Add("@Sugar", DbType.Double).Value = 0.8;
                        command.Parameters.Add("@Coffee", DbType.Double).Value = 0.8;

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
    }
}
