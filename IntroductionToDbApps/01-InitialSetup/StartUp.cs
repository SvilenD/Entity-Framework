namespace InitialSetup
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    public class StartUp
    {
        public const string connectionStringMaster = "Server=.;Database=master;Integrated Security = true;";

        public const string connectionStringMinionsDB = "Server=.;Database=MinionsDB;Integrated Security = true;";

        public const string databaseName = "MinionsDB";

        public static void Main()
        {
            CreateDatabase();

            CreateTables();

            InsertEntries();
        }

        private static void CreateDatabase()
        {
            SqlConnection initialConnection = new SqlConnection(connectionStringMaster);

            initialConnection.Open();

            using (initialConnection)
            {
                try
                {
                    SqlCommand createDb = new SqlCommand($"CREATE DATABASE {databaseName}", initialConnection);

                    createDb.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error during Database creation - " + e.Message);
                }
            }
        }

        private static void CreateTables()
        {
            SqlConnection connection = new SqlConnection(connectionStringMinionsDB);

            connection.Open();

            using (connection)
            {
                using (StreamReader sr = new StreamReader("CreateTables.txt"))
                {
                    while (sr.EndOfStream == false)
                    {
                        string commandText = sr.ReadLine();
                        int nameEnd = commandText.IndexOf('(') - 1;
                        string tableName = commandText.Substring(13, nameEnd - 13);

                        try
                        {
                            SqlCommand createTable = new SqlCommand(commandText, connection);
                            createTable.ExecuteNonQuery();
                            Console.WriteLine($"Table '{tableName}' successfully created.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Creating table '{tableName}' failed - {e.Message}");
                        }
                    }
                }
            }
        }

        private static void InsertEntries()
        {
            SqlConnection connection = new SqlConnection(connectionStringMinionsDB);

            connection.Open();

            using (connection)
            {
                using (StreamReader reader = new StreamReader("TablesEntries.txt"))
                {
                    while (reader.EndOfStream == false)
                    {
                        try
                        {
                            string commandText = reader.ReadLine();
                            SqlCommand insert = new SqlCommand(commandText, connection);
                            int rowsAffected = insert.ExecuteNonQuery();
                            Console.WriteLine($"{rowsAffected} rows affected.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Inserting failed - {e.Message}");
                        }
                    }
                }
            }
        }
    }
}