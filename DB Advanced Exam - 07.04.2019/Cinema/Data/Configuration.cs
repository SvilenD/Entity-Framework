namespace Cinema.Data
{
   public static class Configuration
    {
        public static string ConnectionString = @"Server=.;Database=Cinema;Trusted_Connection=True";

        public const int MinLength = 3;

        public const int MaxLength = 20;

        public const int MinAge = 12;

        public const int MaxAge = 110;

        public const string DecimalMinValue = "0.01";

        public const string DecimalMaxValue = "79228162514264337593543950335";
    }
}
