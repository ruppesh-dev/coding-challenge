using System;
using Microsoft.Data.SqlClient;

namespace PetPalsManagementSystem.util
{
    public static class DBConnUtil
    {
        // Define the connection string directly in the class
        private static string connectionString = "Server=localhost; Database=petpals; Integrated Security=True;TrustServerCertificate=True";

        // Method to establish a connection using the connection string
        public static SqlConnection GetConnection()
        {
            try
            {
                // Create a new SQL connection using the connection string
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();  // Open the connection
                return connection;  // Return the connection object
            }
            catch (Exception ex)
            {
                // Handle any exceptions related to database connection
                throw new Exception("Error establishing a database connection: " + ex.Message);
            }
        }

        // Method to get the connection string directly
        public static string GetDBConnectionString()
        {
            return connectionString;
        }
    }
}
