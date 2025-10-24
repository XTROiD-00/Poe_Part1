using System;
using System.Data.SqlClient;

namespace Poe_Part1.Models
{
    public class ClaimsQueries
    {
        private string connection = @"server=(localdb)\poe_part2;database=claim_stuff;";

        // Create Claims table if it doesn't exist
        public void CreateClaimsTable()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    string query = @"
                        CREATE TABLE Claims (
                            ClaimId INT IDENTITY(1,1) PRIMARY KEY,
                            FacultyName VARCHAR(225) NOT NULL,
                            ModuleName VARCHAR(225) NOT NULL,
                            Sessions TIME NOT NULL,
                            hours TIME NOT NULL,                             
                            rate DECIMAL(10, 2) NOT NULL,                   
                            TotalAmount AS (Sessions * CAST(DATEPART(HOUR, Hours), DATEPART(MINUTE, hours)/60.0 AS DECIMAL) * rate) 
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

        // Store Claim in database
        public void StoreClaim(Claim claim)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    // Trim inputs to avoid storing accidental spaces
                    claim.FacultyName = claim.FacultyName.Trim();
                    claim.ModuleName = claim.ModuleName.Trim();

                    string query = @"
                        INSERT INTO Claims (FacultyName, ModuleName, Sessions, Hours, rate)
                        VALUES (@FacultyName, @ModuleName, @Sessions, @Hours, @rate)";

                    using (SqlCommand insert = new SqlCommand(query, connect))
                    {
                        insert.Parameters.AddWithValue("@FacultyName", claim.FacultyName);
                        insert.Parameters.AddWithValue("@ModuleName", claim.ModuleName);
                        insert.Parameters.AddWithValue("@Sessions", claim.Sessions);
                        insert.Parameters.AddWithValue("@hours", claim.Hours);
                        insert.Parameters.AddWithValue("@rate", claim.Rate);

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

        // Search for claim in the database
        public bool SearchClaim(string FacultyName, string ModuleName, int Sessions, int hours)
        {
            bool found = false;

            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();

                    FacultyName = FacultyName.Trim();
                    ModuleName = ModuleName.Trim();

                    string query = @"
                        SELECT * FROM Claims
                        WHERE LOWER(FacultyName) = LOWER(@FacultyName)
                        AND LOWER(ModuleName) = LOWER(@ModuleName)
                        AND Sessions = @Sessions
                        AND hours = @Hours";

                    using (SqlCommand command = new SqlCommand(query, connect))
                    {
                        command.Parameters.AddWithValue("@FacultyName", FacultyName);
                        command.Parameters.AddWithValue("@ModuleName", ModuleName);
                        command.Parameters.AddWithValue("@Sessions", Sessions);
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

        public List<Claim> GetAllClaims()
        {
            var claims = new List<Claim>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connection))
                {
                    connect.Open();
                    string query = "SELECT * FROM Claims ORDER BY ClaimId DESC";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            claims.Add(new Claim
                            {
                                ClaimId = Convert.ToInt32(reader["ClaimId"]),
                                FacultyName = reader["FacultyName"]?.ToString() ?? "",
                                ModuleName = reader["ModuleName"]?.ToString() ?? "",
                                Sessions = Convert.ToInt32(reader["Sessions"]),
                                Hours = Convert.ToInt32(reader["Hours"]),
                                Rate = Convert.ToDecimal(reader["Rate"]),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                Status = reader["Status"]?.ToString() ?? "Pending",
                                DocumentPath = reader["DocumentPath"]?.ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting claims: " + ex.Message);
            }
            return claims;
        }
    }
}
