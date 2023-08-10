using Moq;
using Retrowars.Data.Repository;
namespace RetroWars.Services.Tests;

using RetroWars.Data.Models;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.ViewModels.Game;
using RetroWars.Web.ViewModels.User;
using static RetroWars.Services.Tests.TestingConstants;

public static class MocksFactory
{

    public static IRepository<Game> CreateMockGameRepository( Game game)
    {
        Mock<IRepository<Game>> mock = new Mock<IRepository<Game>>();

        mock.Setup(gr => gr.GetAllAsync()).ReturnsAsync(new List<Game>() { game});
        mock.Setup(gr => gr.GetOneAsync(gameId, false)).ReturnsAsync(game);
        mock.Setup(gr => gr.AddAsync(game)).ReturnsAsync(true);
        mock.Setup(gr => gr.UpdateOneAsync(game)).ReturnsAsync(true);
        mock.Setup(gr => gr.DeleteOneAsync(Guid.Parse(gameId))).ReturnsAsync(true);
        mock.Setup(gr => gr.SaveAsync()).ReturnsAsync(true);
        

        return mock.Object;
    }

    public static IFireBaseService CreateMockFirebaseService()
    {
        Mock<IFireBaseService> mock = new Mock<IFireBaseService>();
        mock.Setup(fbs => fbs.UploadFile("test", "test", "test")).ReturnsAsync("Uploaded");

        return mock.Object;
    }

    public static IUserService CreateMockUserService(UserViewModel user, GameViewModel gameModel)
    {
        Mock<IUserService> mock = new Mock<IUserService>();
        mock.Setup(us => us.GetFullNameByIdAsync(userId)).ReturnsAsync("Full name");
        mock.Setup(us => us.AllAsync()).ReturnsAsync(new List<UserViewModel>() { user });
        mock.Setup(us => us.GetFullNameByEmailAsync(userId)).ReturnsAsync("Full name");
        mock.Setup(us => us.AddGameToFavoritesAsync(gameId, userId)).ReturnsAsync(true);
        mock.Setup(us => us.RemoveGameFromFavoritesAsync(gameId, userId)).ReturnsAsync(true);
        mock.Setup(us => us.GetApplicationUserFavoritesByIdAsync(userId)).ReturnsAsync(new List<GameViewModel>() { gameModel });
        mock.Setup(us => us.MakeAdmin(userId)).ReturnsAsync(true);

        return mock.Object;
    }

    public static IFileUploadService CreateMockFileUploadService()
    {
        Mock<IFileUploadService> mock = new Mock<IFileUploadService>();

        mock.Setup(fus => fus.UploadFile(null)).ReturnsAsync("Test");
        mock.Setup(fus => fus.ConvertToBase64("Test")).Returns("Test");
        return mock.Object;
    }
}
