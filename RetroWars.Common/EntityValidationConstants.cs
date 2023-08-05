﻿namespace RetroWars.Common
{
    public static class EntityValidationConstants
    {
        public static class Game
        {
            public const int MaxNameLength = 50;

            public const int MaxDeveloperNameLength = 50;

            public const int MaxPublisherNameLength = 50;

            public const int MaxDescriptionLength = 300;

            public const int MaxImageUrlLength= 250;
        }

        public static class Genre
        {
            public const int MaxNameLength = 50;
        }

        public static class Platform
        {
            public const int MaxNameLength = 50;

            public const int MaxCompanyNameLength = 50;

            public const int MaxDescriptionLength = 300;

            public const int MaxImageUrlLength = 250;
        }

        public static class ApplicationUser
        {
            public const int MaxFirstNameLength = 50;
            public const int MaxLastNameLength = 50;
        }
    }
}