namespace RetroWars.Common
{
    public static class EntityValidationConstants
    {
        public static class Game
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 50;

            public const int MinDeveloperNameLength = 2;
            public const int MaxDeveloperNameLength = 50;

            public const int MinPublisherNameLength = 2;
            public const int MaxPublisherNameLength = 50;

            public const int MinDescriptionLength = 10;
            public const int MaxDescriptionLength = 300;

            public const int MinImageUrlLength = 10;
            public const int MaxImageUrlLength= 1000;

            public const string MinYear = "1950";
            public const string MaxYear = "2023";
        }

        public static class Genre
        {
            public const int MinNameLength = 3;
            public const int MaxNameLength = 50;
        }

        public static class Platform
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 50;

            public const int MaxCompanyNameLength = 50;
            public const int MinCompanyNameLength = 3;

            public const int MinDescriptionLength = 5;
            public const int MaxDescriptionLength = 300;

            public const int MaxImageUrlLength = 1000;
            public const int MinImageUrlLenght = 10;

            public const string MinYear = "1950";
            public const string MaxYear = "2023";
        }

        public static class ApplicationUser
        {
            public const int MinFirstNameLength = 2;
            public const int MaxFirstNameLength = 50;

            public const int MinLastNameLength = 2;
            public const int MaxLastNameLength = 50;

            public const int MinPasswordLength = 6;
            public const int MaxPasswordLength = 100;
        }

        public static class ForumPost {
            public const int MaxContentLength = 500;
            public const int MinContentLenght = 10;
        }
        public static class ForumThread
        {
            public const int MaxTitleLength = 60;
            public const int MinTitleLenght = 5;
        }


    }
}