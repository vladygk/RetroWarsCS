namespace RetroWars.Services.Tests;

using Microsoft.AspNetCore.Http;
using RetroWars.Data.Models;
using RetroWars.Web.ViewModels.Game;
using RetroWars.Web.ViewModels.Genre;
using RetroWars.Web.ViewModels.Platform;
using RetroWars.Web.ViewModels.User;
using static TestingConstants;


public static class TestObjectsFactory
{
    public static UserViewModel CreateUser()
    {
        UserViewModel user = new UserViewModel()
        {
            Id = userId,
            Email = "testEmail",
            FullName = "Test Testov",
            IsAdmin = false

        };

        return user;
    }

    public static Game CreateGame()
    {
        Game game = new Game()
        {
            Id = Guid.Parse(gameId),
            Name = "TestGame",
            Developer = "TestDeveloper",
            Publisher = "TestPublisher",
            ImageUrl = "TestUrl",
            YearOfPublishing = 1980,
            GenreId = Guid.Parse(genreId),
            PlatformId = Guid.Parse(platformId),
            Genre = new Genre()
            {
                Id = Guid.Parse(genreId),
                Name = "TestGenre"
            },
            Platform = new Platform()
            {
                Id = Guid.Parse(platformId),
                Name = "TestPlatform",
                ImageUrl = "Test",
                Company = "Test",
                YearOfRelease = 1986,
                Description = "TestDescriptionTestDescriptionTestDescription"
            },
            Description = "TestDescriptionTestDescriptionTestDescription"
        };

        return game;
    }

    public static GameViewModel CreateGameViewModel()
    {
        GameViewModel gameModel = new GameViewModel()
        {
            Id = gameId,
            Name = "TestGame",
            Developer = "TestDeveloper",
            Publisher = "TestPublisher",
            ImageUrl = "TestUrl",
            YearOfPublishing = 1980,
            GenreId = genreId,
            PlatformId = platformId,
            Description = "TestDescriptionTestDescriptionTestDescription",
            Genre = "TestGenre",
            Platform = "TestPlatform"
        };

        return gameModel;
    }

    public static GameFormModel CreateGameFormModel()
    {
        GameFormModel gameModel = new GameFormModel()
        {
            Name = "Test",
            Developer = "Test",
            Description = "TestTestTestTestTestTest",
            Publisher = "Test",
            YearOfPublishing = 1985,
            GenreId = Guid.Parse(genreId),
            PlatformId = Guid.Parse(platformId)
        };

        return gameModel;
    }

    public static PollSelectGameViewModel CreatePollSelectGameViewModel()
    {
        PollSelectGameViewModel pollSelectGameViewModel = new PollSelectGameViewModel()
        {
            Id = gameId,
            Name = "TestGame"
        };
        return pollSelectGameViewModel;
    }


}
