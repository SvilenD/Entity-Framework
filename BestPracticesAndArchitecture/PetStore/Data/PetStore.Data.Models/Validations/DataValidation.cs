namespace PetStore.Data.Models.Validations
{
    public static class DataValidation
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 30;
        public const int DescriptionMaxLength = 1000;
        public const double WeightMinValue = 0.01;
        public const double WeightMaxValue = 100;
        public const double PriceMinValue = 0.1;
        public const double ProfitMinValue = 0.1;
        public const double ProfitMaxValue = 5;

        public static class User
        {
            public const int UserNameMaxLength = 50;
            public const int UserNameMinLength = 5;
            public const int EmailMaxLength = 100;
        }
    }
}