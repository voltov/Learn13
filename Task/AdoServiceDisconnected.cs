using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Task
{
    public class AdoServiceDisconnected
    {
        private readonly string _connectionString;

        public AdoServiceDisconnected(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable GetProducts()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Product";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable productsTable = new DataTable();
                adapter.Fill(productsTable);
                return productsTable;
            }
        }

        public void UpdateProducts(DataTable productsTable)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Product";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(productsTable);
            }
        }

        public DataTable GetOrders()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [Order]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable ordersTable = new DataTable();
                adapter.Fill(ordersTable);
                return ordersTable;
            }
        }

        public void UpdateOrders(DataTable ordersTable)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [Order]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                adapter.Update(ordersTable);
            }
        }
    }
}

