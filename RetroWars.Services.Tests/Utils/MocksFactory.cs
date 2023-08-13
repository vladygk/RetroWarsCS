namespace RetroWars.Services.Tests.Utils;

using Microsoft.AspNetCore.Identity;
using Moq;
using RetroWars.Data.Repository;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.ViewModels.Game;
using RetroWars.Web.ViewModels.User;
using static RetroWars.Services.Tests.Utils.TestingConstants;

public static class MocksFactory
{

    public static IRepository<T> CreateMockRepository<T>(T entity) where T : class, IBaseEntity
    {
        Mock<IRepository<T>> mock = new Mock<IRepository<T>>();

        mock.Setup(gr => gr.GetAllAsync()).ReturnsAsync(new List<T>() { entity });
        mock.Setup(gr => gr.GetOneAsync(entityId, false)).ReturnsAsync(entity);
        mock.Setup(gr => gr.GetOneAsync(testEmail, true)).ReturnsAsync(entity);
        mock.Setup(gr => gr.AddAsync(entity)).ReturnsAsync(true);
        mock.Setup(gr => gr.UpdateOneAsync(entity)).ReturnsAsync(true);
        mock.Setup(gr => gr.DeleteOneAsync(Guid.Parse(entityId))).ReturnsAsync(true);
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
        mock.Setup(us => us.GetFullNameByIdAsync(entityId)).ReturnsAsync("Full name");
        mock.Setup(us => us.AllAsync()).ReturnsAsync(new List<UserViewModel>() { user });
        mock.Setup(us => us.GetFullNameByEmailAsync(entityId)).ReturnsAsync("Full name");
        mock.Setup(us => us.AddGameToFavoritesAsync(entityId, entityId)).ReturnsAsync(true);
        mock.Setup(us => us.RemoveGameFromFavoritesAsync(entityId, entityId)).ReturnsAsync(true);
        mock.Setup(us => us.GetApplicationUserFavoritesByIdAsync(entityId)).ReturnsAsync(new List<GameViewModel>() { gameModel });
        mock.Setup(us => us.MakeAdmin(entityId)).ReturnsAsync(true);

        return mock.Object;
    }

    public static IFileUploadService CreateMockFileUploadService()
    {
        Mock<IFileUploadService> mock = new Mock<IFileUploadService>();

        mock.Setup(fus => fus.UploadFile(null)).ReturnsAsync("Test");
        mock.Setup(fus => fus.ConvertToBase64("Test")).Returns("Test");
        return mock.Object;
    }
    public static UserManager<T> CreateMockUserManager<T>(List<T> ls) where T : class
    {
        var store = new Mock<IUserStore<T>>();
        var mock = new Mock<UserManager<T>>(store.Object, null, null, null, null, null, null, null, null);
        mock.Object.UserValidators.Add(new UserValidator<T>());
        mock.Object.PasswordValidators.Add(new PasswordValidator<T>());

        mock.Setup(x => x.DeleteAsync(It.IsAny<T>())).ReturnsAsync(IdentityResult.Success);
        mock.Setup(x => x.CreateAsync(It.IsAny<T>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<T, string>((x, y) => ls.Add(x));
        mock.Setup(x => x.UpdateAsync(It.IsAny<T>())).ReturnsAsync(IdentityResult.Success);

        return mock.Object;
    }

}
