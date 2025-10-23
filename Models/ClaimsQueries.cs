using System;
using System.Data.SqlClient;

namespace Poe_Part1.Models
{
    public class ClaimsQueries
    {
        private string connection = @"server=(localdb)\poe_part2;database=claim_stuff;";

        // Method to create the claims table (adjusted for new schema)
        public void CreateClaimsTable()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    string query = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Claims' AND xtype='U')
                        CREATE TABLE Claims (
                            ClaimId INT IDENTITY(1,1) PRIMARY KEY,
                            Faculty_name VARCHAR(225) NOT NULL,
                            Module_name VARCHAR(225) NOT NULL,
                            Sessions INT NOT NULL,
                            Hours TIME NOT NULL,                             
                            Price DECIMAL(10, 2) NOT NULL,                   
                            TotalAmount AS (Sessions * CAST(DATEPART(HOUR, Hours) + DATEPART(MINUTE, Hours)/60.0 AS DECIMAL(10,2)) * Price) PERSISTED
                        )";

                    using (SqlCommand create = new SqlCommand(query, connect))
                    {
                        create.ExecuteNonQuery();
                    }

                    connect.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error creating table: " + error.Message);
            }
        }

        // Method to store a claim in the database
        public void StoreClaim(string facultyName, string moduleName, int sessions, TimeSpan hours, decimal price)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    // Trim inputs to avoid storing accidental spaces
                    facultyName = facultyName.Trim();
                    moduleName = moduleName.Trim();

                    string query = @"
                        INSERT INTO Claims (Faculty_name, Module_name, Sessions, Hours, Price)
                        VALUES (@facultyName, @moduleName, @sessions, @hours, @price)";

                    using (SqlCommand insert = new SqlCommand(query, connect))
                    {
                        insert.Parameters.AddWithValue("@facultyName", facultyName);
                        insert.Parameters.AddWithValue("@moduleName", moduleName);
                        insert.Parameters.AddWithValue("@sessions", sessions);
                        insert.Parameters.AddWithValue("@hours", hours);
                        insert.Parameters.AddWithValue("@price", price);

                        insert.ExecuteNonQuery();
                    }

                    connect.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error storing claim: " + error.Message);
            }
        }

        // Method to search for a claim (if needed, adjust for claims-specific searches)
        public bool SearchClaim(string facultyName, string moduleName, int sessions, TimeSpan hours)
        {
            bool found = false;

            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    facultyName = facultyName.Trim();
                    moduleName = moduleName.Trim();

                    string query = @"
                        SELECT * FROM Claims
                        WHERE LOWER(Faculty_name) = LOWER(@facultyName)
                        AND LOWER(Module_name) = LOWER(@moduleName)
                        AND Sessions = @sessions
                        AND Hours = @hours";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@facultyName", facultyName);
                        command.Parameters.AddWithValue("@moduleName", moduleName);
                        command.Parameters.AddWithValue("@sessions", sessions);
                        command.Parameters.AddWithValue("@hours", hours);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                found = true;
                            }
                        }
                    }

                    connect.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error during search: " + error.Message);
            }

            return found;
        }
    }
}
