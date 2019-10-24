namespace AddMinion
{
    using System;
    using System.Data.SqlClient;

    public class AddMinion
    {
        public static void Main()
        {
            string connectionString = "Server=.;Database=MinionsDB;Integrated Security = true;";

            string[] minionData = Console.ReadLine().Split();

            string minionName = minionData[1];
            int minionAge = int.Parse(minionData[2]);
            string townName = minionData[3];

            string villainName = Console.ReadLine().Split()[1];

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using (connection)
            {
                try
                {
                    int townId = GetId("Towns", townName, connection);
                    if (townId == -1)
                    {
                        InsertTown(townName, connection);
                        townId = GetId("Towns", townName, connection);
                    }

                    int villainId = GetId("Villains", villainName, connection);
                    if (villainId == -1)
                    {
                        InsertVillain(villainName, connection);
                        villainId = GetId("Villains", villainName, connection);
                    }

                    int minionId = GetId("Minions", minionName, connection);
                    if (minionId == -1)
                    {
                        InsertMinion(minionName, minionAge, townId, connection);
                        minionId = GetId("Minions", minionName, connection);
                    }

                    SqlCommand addMinionToVillain = new SqlCommand("INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId);", connection);
                    addMinionToVillain.Parameters.AddWithValue("minionId", minionId);
                    addMinionToVillain.Parameters.AddWithValue("villainId", villainId);
                    addMinionToVillain.ExecuteNonQuery();

                    Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static int GetId(string tableName, string name, SqlConnection connection)
        {
            SqlCommand getId = new SqlCommand($"SELECT Id FROM {tableName} WHERE Name = @Name;", connection);
            getId.Parameters.AddWithValue("@Name", name);
            return (int)(getId.ExecuteScalar() ?? -1);
        }

        private static void InsertTown(string name, SqlConnection connection)
        {
            SqlCommand insert = new SqlCommand("INSERT INTO Towns (Name) VALUES(@name);", connection);
            insert.Parameters.AddWithValue("@name", name);
            insert.ExecuteNonQuery();
            Console.WriteLine($"Town {name} was added to the database.");
        }

        private static void InsertVillain(string name, SqlConnection connection)
        {
            SqlCommand insert = new SqlCommand("INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4);", connection);
            insert.Parameters.AddWithValue("@villainName", name);
            insert.ExecuteNonQuery();
            Console.WriteLine($"Villain {name} was added to the database.");
        }

        private static void InsertMinion(string minionName, int minionAge, int townId, SqlConnection connection)
        {
            SqlCommand insert = new SqlCommand("INSERT INTO Minions (Name, Age, TownId) VALUES (@minionName, @minionAge, @townId);", connection);
            insert.Parameters.AddWithValue("@minionName", minionName);
            insert.Parameters.AddWithValue("@minionAge", minionAge);
            insert.Parameters.AddWithValue("@townId", townId);
            insert.ExecuteNonQuery();
        }
    }
}