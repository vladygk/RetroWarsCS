namespace RetroWars.Services.Tests.Utils;

using RetroWars.Data.Models;
using RetroWars.Web.App.Areas.Admin.ViewModels;
using RetroWars.Web.ViewModels.Game;
using RetroWars.Web.ViewModels.Genre;
using RetroWars.Web.ViewModels.Platform;
using RetroWars.Web.ViewModels.Poll;
using RetroWars.Web.ViewModels.User;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using static TestingConstants;


public static class TestObjectsFactory
{
    public static UserViewModel CreateUserViewModel()
    {
        UserViewModel user = new UserViewModel()
        {
            Id = entityId,
            Email = "testEmail",
            FullName = "Test Testov",
            IsAdmin = false

        };

        return user;
    }

    public static ApplicationUser CreateApplicationUser()
    {
        ApplicationUser user = new ApplicationUser()
        {
            Id = Guid.Parse(entityId),
            FirstName = "Test",
            LastName = "Testov",
            Email = "testEmail",
            FavoriteGames = new List<Game> { CreateGame() }
        };
        return user;
    }

    public static Game CreateGame()
    {
        Game game = new Game()
        {
            Id = Guid.Parse(entityId),
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
            Id = entityId,
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
            Id = entityId,
            Name = "TestGame"
        };
        return pollSelectGameViewModel;
    }

    public static Poll CreatePoll()
    {
        Poll poll = new Poll()
        {
            Id = Guid.Parse(entityId),
            FirstGameId = Guid.Parse(entityId),
            IsActive = true,
            FirstGame = CreateGame(),
            SecondGameId = Guid.Parse(entityId),
            SecondGame = CreateGame(),
            VotesForFirst = 0,
            VotesForSecond = 0,
        };

        return poll;
    }

    public static PollViewModel CreatePollViewModel(bool withVote, bool voteForFirst)
    {
        PollViewModel pollViewModel = new PollViewModel()
        {
            Id = Guid.Parse(entityId),
            IsActive = true,
            FirstGameId = Guid.Parse(entityId),
            FirstGameName = "TestGame",
            FirstGamePublisher = "TestPublisher",
            FirstGamePlatform = "TestPlatform",
            FirstGameImageUrl = "TestUrl",
            VotesForFirst = 0,
            VotesForSecond = 0,
            SecondGameId = Guid.Parse(entityId),
            SecondGameName = "TestGame",
            SecondGamePublisher = "TestPublisher",
            SecondGameImageUrl = "TestUrl",
            SecondGamePlatform = "TestPlatform",

        };
        if (withVote && voteForFirst)
        {
            pollViewModel.VotesForFirst = 1;
        }
        else if (withVote)
        {
            pollViewModel.VotesForSecond = 1;
        }

        return pollViewModel;
    }

    public static PollFormModel CreatePollFormModel()
    {
        PollFormModel pollFormModel = new PollFormModel()
        {
            FirstGameId = entityId,
            SecondGameId = entityId
        };

        return pollFormModel;
    }

    public static PollAdminViewModel CreatePollAdminViewModel()
    {
        PollAdminViewModel pollAdminViewModel = new PollAdminViewModel()
        {
            Id = Guid.Parse(entityId),
            IsActive = true,
            FirstGameName = "TestGame",
            FirstGamePublisher = "TestPublisher",
            FirstGamePlatform = "TestPlatform",
            VotesForFirst = 0,
            VotesForSecond = 0,
            SecondGameName = "TestGame",
            SecondGamePublisher = "TestPublisher",
            SecondGamePlatform = "TestPlatform",
        };

        return pollAdminViewModel;
    }

    public static Genre CreateGenre()
    {
        Genre genre = new Genre()
        {
            Id = Guid.Parse(entityId),
            Name = "GenreTest"
        };
        return genre;
    }
    public static GameSelectGenreFormModel CreateGameSelectGenreFormModel()
    {
        GameSelectGenreFormModel gameSelectGenreFormModel = new GameSelectGenreFormModel()
        {
            Id = Guid.Parse(entityId),
            Name = "GenreTest"
        };

        return gameSelectGenreFormModel;
    }

    public static Platform CreatePlatform()
    {
        Platform platform = new Platform()
        {
            Id = Guid.Parse(entityId),
            Name = "TestPlatform",
            ImageUrl = "TetUrl",
            Company = "TestCompany",
            Description = "TestDescription",
            YearOfRelease = 2000
        };
        return platform;
    }

    public static PlatformViewModel CreatePlatformViewModel()
    {
        PlatformViewModel platformViewModel = new PlatformViewModel()
        {
            Id = Guid.Parse(entityId),
            Name = "TestPlatform",
            ImageUrl = "TetUrl",
            Company = "TestCompany",
            Description = "TestDescription",
            YearOfRelease = 2000,
            Games = String.Empty
        };

        return platformViewModel;
    }
    public static GameSelectPlatformsFormModel CreateGameSelectPlatformsFormModel()
    {
        GameSelectPlatformsFormModel gameSelectPlatformsFormModel = new GameSelectPlatformsFormModel()
        {
            Id = Guid.Parse(entityId),
            Name = "TestPlatform"
        };
        return gameSelectPlatformsFormModel;
    }
}
