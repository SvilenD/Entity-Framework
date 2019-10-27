namespace ChangeTownNamesCasing
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            string connectionString = "Server=.;Database=MinionsDB;Integrated Security = true;";

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string countryName = Console.ReadLine();
            using (connection)
            {
                SqlCommand setTownNamesToUpper = new SqlCommand("UPDATE Towns " +
                                                                "SET Name = UPPER(Name) " +
                                                                    "WHERE CountryCode = " +
                                                                    "(SELECT c.Id FROM Countries AS c " +
                                                                        "WHERE c.Name = @countryName)", connection);

                setTownNamesToUpper.Parameters.AddWithValue("@countryName", countryName);

                int rowsAffected = setTownNamesToUpper.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    Console.WriteLine("No town names were affected.");
                }
                else
                {
                    Console.WriteLine($"{rowsAffected} town names were affected.");

                    SqlCommand getTownNames = new SqlCommand("SELECT t.Name " +
                                                                "FROM Towns as t " +
                                                                "JOIN Countries AS c " +
                                                                "ON c.Id = t.CountryCode " +
                                                                "WHERE c.Name = @countryName", connection);
                    getTownNames.Parameters.AddWithValue("@countryName", countryName);

                    SqlDataReader reader = getTownNames.ExecuteReader();

                    List<string> towns = new List<string>();

                    while (reader.Read())
                    {
                        towns.Add((string)reader[0]);
                    }

                    Console.WriteLine("[" + string.Join(", ", towns) + "]");
                }
                
            }
        }
    }
}