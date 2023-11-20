using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        // Connection string for the SQL Server database
        string connectionString = "Data Source=Amaze\\SQLEXPRESS;Initial Catalog=things_to_do;Integrated Security=True";

        // Variables for the WHERE clause
        int categoryId = 1;
        string name = "lesson";

        // SQL SELECT query with a parameterized WHERE clause
        string sqlQuery = "SELECT * FROM dbo.tbl_items WHERE itid = @CategoryId AND name = @ItemName";

        // Create a SqlConnection and execute the parameterized query
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                // Add parameters to the query
                command.Parameters.AddWithValue("@CategoryId", categoryId);
                command.Parameters.AddWithValue("@ItemName", name);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Check if there are rows returned
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Access columns using reader
                            int itid = reader.GetInt32(0);
                            string itemNameResult = reader.GetString(1);

                            Console.WriteLine($"Item ID: {itid}, Item Name: {itemNameResult}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                }
            }
        }
    }
}

