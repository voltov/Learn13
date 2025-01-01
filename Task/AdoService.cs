using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Task
{
    public class AdoService
    {
        private readonly string _connectionString;

        public AdoService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CreateProduct(string name, string description, decimal weight, decimal height, decimal width, decimal length)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Product (Name, Description, Weight, Height, Width, Length) OUTPUT INSERTED.Id VALUES (@Name, @Description, @Weight, @Height, @Width, @Length)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Weight", weight);
                cmd.Parameters.AddWithValue("@Height", height);
                cmd.Parameters.AddWithValue("@Width", width);
                cmd.Parameters.AddWithValue("@Length", length);
                conn.Open();
                int insertedId = (int)cmd.ExecuteScalar();
                return insertedId;
            }
        }


        public void UpdateProduct(int id, string name, string description, decimal weight, decimal height, decimal width, decimal length)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Product SET Name = @Name, Description = @Description, Weight = @Weight, Height = @Height, Width = @Width, Length = @Length WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Weight", weight);
                cmd.Parameters.AddWithValue("@Height", height);
                cmd.Parameters.AddWithValue("@Width", width);
                cmd.Parameters.AddWithValue("@Length", length);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Product WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Product GetProduct(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Product WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Description = (string)reader["Description"],
                            Weight = (decimal)reader["Weight"],
                            Height = (decimal)reader["Height"],
                            Width = (decimal)reader["Width"],
                            Length = (decimal)reader["Length"]
                        };
                    }
                }
            }
            return null;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Product";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Description = (string)reader["Description"],
                            Weight = (decimal)reader["Weight"],
                            Height = (decimal)reader["Height"],
                            Width = (decimal)reader["Width"],
                            Length = (decimal)reader["Length"]
                        });
                    }
                }
            }
            return products;
        }

        public int CreateOrder(string status, DateTime createdDate, DateTime updatedDate, int productId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO [Order] (Status, CreatedDate, UpdatedDate, ProductId) OUTPUT INSERTED.Id VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductId)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@CreatedDate", createdDate);
                cmd.Parameters.AddWithValue("@UpdatedDate", updatedDate);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                conn.Open();
                int insertedId = (int)cmd.ExecuteScalar();
                return insertedId;
            }
        }


        public void UpdateOrder(int id, string status, DateTime createdDate, DateTime updatedDate, int productId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE [Order] SET Status = @Status, CreatedDate = @CreatedDate, UpdatedDate = @UpdatedDate, ProductId = @ProductId WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@CreatedDate", createdDate);
                cmd.Parameters.AddWithValue("@UpdatedDate", updatedDate);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteOrder(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM [Order] WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Order GetOrder(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [Order] WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Order
                        {
                            Id = (int)reader["Id"],
                            Status = (string)reader["Status"],
                            CreatedDate = (DateTime)reader["CreatedDate"],
                            UpdatedDate = (DateTime)reader["UpdatedDate"],
                            ProductId = (int)reader["ProductId"]
                        };
                    }
                }
            }
            return null;
        }

        public List<Order> GetOrders(int? month = null, int? year = null, string status = null, int? productId = null)
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM [Order] WHERE (@Month IS NULL OR MONTH(CreatedDate) = @Month) AND (@Year IS NULL OR YEAR(CreatedDate) = @Year) AND (@Status IS NULL OR Status = @Status) AND (@ProductId IS NULL OR ProductId = @ProductId)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Month", (object)month ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Year", (object)year ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", (object)status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ProductId", (object)productId ?? DBNull.Value);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            Id = (int)reader["Id"],
                            Status = (string)reader["Status"],
                            CreatedDate = (DateTime)reader["CreatedDate"],
                            UpdatedDate = (DateTime)reader["UpdatedDate"],
                            ProductId = (int)reader["ProductId"]
                        });
                    }
                }
            }
            return orders;
        }

        public void DeleteOrders(int? month = null, int? year = null, string status = null, int? productId = null)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM [Order] WHERE (@Month IS NULL OR MONTH(CreatedDate) = @Month) AND (@Year IS NULL OR YEAR(CreatedDate) = @Year) AND (@Status IS NULL OR Status = @Status) AND (@ProductId IS NULL OR ProductId = @ProductId)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Month", (object)month ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Year", (object)year ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", (object)status ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ProductId", (object)productId ?? DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
