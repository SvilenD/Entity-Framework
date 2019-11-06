namespace Lab.Data
{
    public static class Validations
    {
        public static class Make
        {
            public const int NameMaxLength = 20;
        }

        public static class Model
        {
            public const int NameMaxLength = 20;

            public const int ModificationMaxLength = 30;
        }

        public static class Car
        {
            public const string VinRequirement = "CHAR(17)";
        }

        public static class Customer
        {
            public const int NameMaxLength = 30;
        }

        public static class Address
        {
            public const int TownMaxLength = 30;
        }
    }
}