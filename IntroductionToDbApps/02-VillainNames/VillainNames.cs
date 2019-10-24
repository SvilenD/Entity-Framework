namespace VillainNames
{
    using System;
    using System.Data.SqlClient;

    public class VillainNames
    {
        public const string connectionStringMinionsDB = "Server=.;Database=MinionsDB;Integrated Security = true;";

        public static void Main()
        {
            SqlConnection connection = new SqlConnection(connectionStringMinionsDB);

            connection.Open();

            using (connection)
            {
                SqlCommand getNamesAndMinionsCount = new SqlCommand(" SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount " +
                                                                "FROM Villains AS v JOIN MinionsVillains AS mv ON v.Id = mv.VillainId " +
                                                                "GROUP BY v.Id, v.Name " +
                                                                "HAVING COUNT(mv.VillainId) >= 3 " + //changed to >=
                                                                "ORDER BY COUNT(mv.VillainId) DESC", connection); //Ordered DESC
                SqlDataReader reader = getNamesAndMinionsCount.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]} - {reader["MinionsCount"]}"); 
                    //Expected Result => "Jilly – 4", "Dobromir - 3", "Gru - 3"
                }
            }
        }
    }
}