namespace IncreaseAgeStoredProcedure
{
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public const string connectionString = "Server=.;Database=MinionsDB;Integrated Security = true;";
        public const string sp_IncreaseAge = "EXEC usp_GetOlder @id";
        public const string query_GetInfo = "SELECT Name, Age FROM Minions WHERE Id = @Id";

        public static void Main()
        {

            int minionId = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            using (connection)
            {
                SqlCommand increaseAge = new SqlCommand(sp_IncreaseAge, connection);
                increaseAge.Parameters.AddWithValue("@id", minionId);
                increaseAge.ExecuteNonQuery();
                increaseAge.Dispose();

                SqlCommand getMinionInfo = new SqlCommand(query_GetInfo, connection);
                getMinionInfo.Parameters.AddWithValue("@id", minionId);

                SqlDataReader result = getMinionInfo.ExecuteReader();
                result.Read();

                Console.WriteLine($"{result["Name"]} - {result["Age"]} years old");
            }
        }
    }
}