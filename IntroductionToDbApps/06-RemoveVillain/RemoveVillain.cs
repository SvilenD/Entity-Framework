namespace RemoveVillain
{
    using System;
    using System.Data.SqlClient;

    public class RemoveVillain
    {
        public static void Main()
        {
            string connectionString = "Server=.;Database=MinionsDB;Integrated Security = true;";

            int id = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            using (connection)
            {
                SqlTransaction transaction = connection.BeginTransaction();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "SELECT Name FROM Villains WHERE Id = @villainId;";
                    command.Parameters.AddWithValue("@villainId", id);

                    string villainName = (string)command.ExecuteScalar();
                    if (villainName == null)
                    {
                        throw new ArgumentException("No such villain was found.");
                    }

                    command.CommandText = "DELETE FROM MinionsVillains WHERE VillainId = @villainId";
                    int minionsReleased = command.ExecuteNonQuery();

                    command.CommandText = "DELETE FROM Villains WHERE Id = @villainId";
                    command.ExecuteNonQuery();

                    transaction.Commit();

                    Console.WriteLine($"{villainName} was deleted.");
                    Console.WriteLine($"{minionsReleased} minions were released.");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}