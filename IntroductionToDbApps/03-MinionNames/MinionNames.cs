namespace MinionNames
{
    using System;
    using System.Data.SqlClient;

    public class MinionNames
    {
        public const string connectionStringMinionsDB = "Server=.;Database=MinionsDB;Integrated Security = true;";

        public static void Main()
        {
            SqlConnection connection = new SqlConnection(connectionStringMinionsDB);

            connection.Open();

            using (connection)
            {
                int id = int.Parse(Console.ReadLine());
                SqlCommand getVillainName = new SqlCommand("SELECT Name FROM Villains WHERE Id = @Id", connection);
                getVillainName.Parameters.AddWithValue("@Id", id);

                string villainName = (string)getVillainName.ExecuteScalar();
                if (villainName != null)
                {
                    Console.WriteLine($"Villain: {villainName}");
                }
                else
                {
                    Console.WriteLine($"No villain with ID {id} exists in the database.");
                }

                SqlCommand getMinions = new SqlCommand("SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum, m.Name, m.Age " +
                                                            "FROM MinionsVillains AS mv " +
                                                            "JOIN Minions As m ON mv.MinionId = m.Id " +
                                                            "WHERE mv.VillainId = @Id ORDER BY m.Name;", connection);
                getMinions.Parameters.AddWithValue("Id", id);

                SqlDataReader reader = getMinions.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["RowNum"]}. {reader["Name"]} {reader["Age"]}");
                }
            }
        }
    }
}