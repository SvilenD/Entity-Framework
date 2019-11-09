namespace BookShop.Models
{
    public static class ValidationSettings
    {
        public static class Book
        {
            public const int TitleMaxLegnth = 50;

            public const int DescriptionMaxLength = 1000;
        }

        public static class Category
        {
            public const int NameMaxLegnth = 50;
        }

        public static class Author
        {
            public const int NameMaxLegnth = 50;
        }
    }
}