using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ConsoleApp84.Data
{
    // This class is basically for connecting to the database and actaully carrying out sql commands and queries 
    public static class DatabaseHelper
    {

        private static string connectionString = "Server=localhost\\SQLEXPRESS;Database=DatingAppDBSF;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";

        // This method basically Creates and returns a SQL connection
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // this method carries out  the following commands insert update and delete
        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    command.CommandTimeout = 30;
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing non-query: " + ex.Message);
                // Optionally rethrow or handle the error as needed.
                throw;
            }
        }

        // this method carries out the following command select that bascally return data 
        public static DataTable ExecuteQuery(string sql, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    command.CommandTimeout = 30; // Set timeout to 30 seconds.
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        connection.Open();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error executing query: " + ex.Message);

                throw;
            }
        }
    }
}
