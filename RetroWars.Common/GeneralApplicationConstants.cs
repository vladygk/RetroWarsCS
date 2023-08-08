namespace RetroWars.Common;
public static class GeneralApplicationConstants
{
    public const string AdminRoleName = "Administrator";
    public const string AdminEmail = "admin@admin.bg";

    public const string GamesCacheKey = "GamesCache";
    public const int GamesCacheDurationMinutes = 5;

    public const string DefaultFireBaseStorageFolder = "Images";
    public const int MaxNumberOfGamesToDisplayForPlatform = 10;
    
    public static class GameFormPartialModes
    {
        public const string EditMode = "Edit";
        public const string AddMode = "Add";
        public const string DeleteMode = "Delete";
    }
}



