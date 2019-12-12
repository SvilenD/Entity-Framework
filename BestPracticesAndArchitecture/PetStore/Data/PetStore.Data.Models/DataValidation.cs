namespace PetStore.Data.Models
{
    public static class DataValidation
    {
        public const int NameMinLength = 3;
        public const int NameMaxLength = 30;
        public const int DescriptionMaxLength = 1000;
        
        public static class User
        {
            public const int UserNameMaxLength = 50;
            public const int UserNameMinLength = 5;
            public const int EmailMaxLength = 100;
        }
    }
}