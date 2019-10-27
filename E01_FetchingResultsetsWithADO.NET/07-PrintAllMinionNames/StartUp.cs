namespace PrintAllMinionNames
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

            using (connection)
            {
                SqlCommand getNames = new SqlCommand("SELECT Name FROM Minions", connection);
                SqlDataReader reader = getNames.ExecuteReader();

                List<string> names = new List<string>();
                while (reader.Read())
                {
                    names.Add((string)reader[0]);
                }

                for (int i = 0; i < names.Count / 2; i++)
                {
                    Console.WriteLine(names[i]);
                    Console.WriteLine(names[names.Count - 1 - i]);
                }

                if (names.Count % 2 != 0)
                {
                    Console.WriteLine(names[names.Count / 2]);
                }
            }
        }
    }
}