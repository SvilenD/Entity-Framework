namespace AddMinion
{
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public const string connectionString = "Server=.;Database=MinionsDB;Integrated Security = true;";

        public static void Main()
        {
            string[] minionData = Console.ReadLine().Split();

            string minionName = minionData[1];
            int minionAge = int.Parse(minionData[2]);
            string townName = minionData[3];

            string villainName = Console.ReadLine().Split()[1];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    int townId = GetId("Towns", townName, connection, transaction);
                    if (townId == -1)
                    {
                        InsertTown(townName, connection, transaction);
                        townId = GetId("Towns", townName, connection, transaction);
                    }

                    int villainId = GetId("Villains", villainName, connection, transaction);
                    if (villainId == -1)
                    {
                        InsertVillain(villainName, connection, transaction);
                        villainId = GetId("Villains", villainName, connection, transaction);
                    }

                    int minionId = GetId("Minions", minionName, connection, transaction);
                    if (minionId == -1)
                    {
                        InsertMinion(minionName, minionAge, townId, connection, transaction);
                        minionId = GetId("Minions", minionName, connection, transaction);
                    }

                    SqlCommand addMinionToVillain = new SqlCommand("INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId);", connection, transaction);
                    addMinionToVillain.Parameters.AddWithValue("minionId", minionId);
                    addMinionToVillain.Parameters.AddWithValue("villainId", villainId);
                    addMinionToVillain.ExecuteNonQuery();

                    transaction.Commit();
                    Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                }
            }
        }

        private static int GetId(string tableName, string name, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand getId = new SqlCommand($"SELECT Id FROM {tableName} WHERE Name = @Name;", connection, transaction);
            getId.Parameters.AddWithValue("@Name", name);
            return (int)(getId.ExecuteScalar() ?? -1);
        }

        private static void InsertTown(string name, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand insert = new SqlCommand("INSERT INTO Towns (Name) VALUES(@name);", connection, transaction);
            insert.Parameters.AddWithValue("@name", name);
            insert.ExecuteNonQuery();
            Console.WriteLine($"Town {name} was added to the database.");
        }

        private static void InsertVillain(string name, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand insert = new SqlCommand("INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4);", connection, transaction);
            insert.Parameters.AddWithValue("@villainName", name);
            insert.ExecuteNonQuery();
            Console.WriteLine($"Villain {name} was added to the database.");
        }

        private static void InsertMinion(string minionName, int minionAge, int townId, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand insert = new SqlCommand("INSERT INTO Minions (Name, Age, TownId) VALUES (@minionName, @minionAge, @townId);", connection, transaction);
            insert.Parameters.AddWithValue("@minionName", minionName);
            insert.Parameters.AddWithValue("@minionAge", minionAge);
            insert.Parameters.AddWithValue("@townId", townId);
            insert.ExecuteNonQuery();
        }
    }
}