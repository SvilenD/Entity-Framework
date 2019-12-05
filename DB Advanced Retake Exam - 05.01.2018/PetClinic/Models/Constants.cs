namespace PetClinic.Models
{
    public static class Constants
    {
        public static class Animal
        {
            public const int MinLength = 3;

            public const int MaxLength = 20;
        }

        public static class Passport
        {
            public const int MinLength = 3;

            public const int MaxLength = 30;
        }

        public static class Vet
        {
            public const int MinLength = 3;

            public const int NameMaxLength = 40;

            public const int ProfessionMaxLength = 50;

            public const int MinAge = 22;

            public const int MaxAge = 65;
        }

        public static class AnimalAid
        {
            public const int MinLength = 3;

            public const int MaxLength = 30;

            public const string PriceMinValue = "0.01";

            public const string PriceMaxValue = "79228162514264337593543950335";
        }
    }
}