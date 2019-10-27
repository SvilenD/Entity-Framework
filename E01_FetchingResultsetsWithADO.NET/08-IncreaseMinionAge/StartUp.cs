namespace IncreaseMinionAge
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            int[] minionsId = Console.ReadLine()
                             .Split()
                             .Select(int.Parse)
                             .ToArray();

            string connectionString = "Server=.;Database=MinionsDB;Integrated Security = true;";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            using (connection)
            {
                foreach (var id in minionsId)
                {
                    SqlCommand increaseAndTitleCase = new SqlCommand(" UPDATE Minions " +
                                                                    "SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1 " +
                                                                    "WHERE Id = @Id;", connection);
                    increaseAndTitleCase.Parameters.AddWithValue("@Id", id);
                    increaseAndTitleCase.ExecuteNonQuery();
                }

                SqlCommand getAllNamesAges = new SqlCommand("SELECT Name, Age FROM Minions", connection);
                SqlDataReader reader = getAllNamesAges.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]} {reader["Age"]}");
                }
            }
        }
    }
}