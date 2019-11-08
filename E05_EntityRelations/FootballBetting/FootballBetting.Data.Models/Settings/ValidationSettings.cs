namespace P03_FootballBetting.Data.Models.Configurations
{
    public static class ValidationSettings
    {
        public static class Bet
        {
            public const int PredictionMaxLength = 7;
        }

        public static class Color
        {
            public const int NameMaxLength = 30;
        }

        public static class Country
        {
            public const int NameMaxLength = 50;
        }

        public static class Town
        {
            public const int NameMaxLength = 30;
        }

        public static class Position
        {
            public const int NameMaxLength = 30;
        }

        public static class Team
        {
            public const int NameMaxLength = 50;
        }

        public static class Player
        {
            public const int NameMaxLength = 50;
        }

        public static class User
        {
            public const int User_Pass_EmailMaxLength = 30;

            public const int NameMaxLength = 50;
        }
    }
}