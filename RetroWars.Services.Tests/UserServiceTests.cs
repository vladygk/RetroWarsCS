using RetroWars.Services.Tests.Utils;

namespace RetroWars.Services.Tests;

using Microsoft.AspNetCore.Identity;
using RetroWars.Data.Repository;
using RetroWars.Data.Models;
using RetroWars.Services.Data;
using RetroWars.Services.Data.Contracts;
using RetroWars.Services.Tests.Utils;
using RetroWars.Web.ViewModels.Game;
using RetroWars.Web.ViewModels.User;
using System.Text.Json;
using static TestingConstants;


[TestFixture]
public class UserServiceTests
{
    private IUserService userService;
    private IRepository<ApplicationUser> applicationUserRepository;
    private IRepository<Game> gameRepository;
    private UserManager<ApplicationUser> userManager;

    private GameViewModel gameViewModel;
    private ApplicationUser applicationUser;
    private UserViewModel userViewModel;
    private Game game;
    [SetUp]
    public void Setup()
    {
        this.applicationUser = TestObjectsFactory.CreateApplicationUser();
        this.game = TestObjectsFactory.CreateGame();
        this.userViewModel = TestObjectsFactory.CreateUserViewModel();

        this.gameViewModel = TestObjectsFactory.CreateGameViewModel();
        this.userManager = MocksFactory.CreateMockUserManager<ApplicationUser>(new List<ApplicationUser>());
        this.applicationUserRepository = MocksFactory.CreateMockRepository<ApplicationUser>(this.applicationUser);
        this.gameRepository = MocksFactory.CreateMockRepository<Game>(game);
        this.userService = new UserService(this.applicationUserRepository, this.gameRepository, this.userManager);
    }

    [Test]
    public async Task GetFullNameByIdAsyncWorksCorrectly()
    {
        // Arrange
        string expected = testUserName;

        // Act
        string actual = await this.userService.GetFullNameByIdAsync(entityId);

        // Assert

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetFullNameByIdAsyncWithInvalidIdl()
    {
        // Arrange
        string expected = String.Empty;

        // Act
        string actual = await this.userService.GetFullNameByIdAsync(invalidId);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public async Task AllAsyncWorksCorrectly()
    {
        // Arrange
        var expected = new List<UserViewModel>()
        {
        this.userViewModel
        };

        // Act
        var actual = await this.userService.AllAsync();

        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);

        // Assert

        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }

    [Test]
    public async Task GetFullNameByEmailAsyncWorksCorrectly()
    {
        // Arrange
        string expected = testUserName;

        // Act
        string actual = await this.userService.GetFullNameByEmailAsync(testEmail);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetFullNameByEmailAsyncWithInvalidEmaill()
    {
        // Arrange
        string expected = String.Empty;

        // Act
        string actual = await this.userService.GetFullNameByEmailAsync(invalidEmail);

        // Assert
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public async Task AddGameToFavoritesAsyncWorksCorrectly()
    {
        // Arrange
       

        // Act
        bool actual = await this.userService.AddGameToFavoritesAsync(entityId,entityId);

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public async Task AddGameToFavoritesAsyncWithInvalidInput()
    {
      
        // Act
        bool actual =  await  this.userService.AddGameToFavoritesAsync(invalidId, invalidId);
        
        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public async Task RemoveGameFromFavoritesAsyncWorksCorrectly()
    {
        // Arrange


        // Act
        bool actual = await this.userService.RemoveGameFromFavoritesAsync(entityId,entityId);

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public async Task RemoveGameFromFavoritesAsyncWithInvalidInput()
    {

        // Act
        bool actual = await this.userService.RemoveGameFromFavoritesAsync(invalidId, invalidId);

        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public async Task GetApplicationUserFavoritesByIdAsyncWorksCorrectly()
    {
        // Arrange
        var expected = new List<GameViewModel>()
        {
            this.gameViewModel
        };

        // Act
        var actual = await this.userService.GetApplicationUserFavoritesByIdAsync(entityId);

        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        // Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }

    [Test]
    public async Task GetApplicationUserFavoritesByIdAsyncThrowsWithInvalidId()
    {
        Assert.ThrowsAsync<ArgumentException>(async () => {
           await this.userService.GetApplicationUserFavoritesByIdAsync(invalidId);
        }, "Invalid Id.");
    }


    [Test]
    public async Task MakeAdminWorksCorrectly()
    {
        // Act
        bool actual = await this.userService.MakeAdmin(entityId);

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public async Task MakeAdminWithInvalidId()
    {
        // Act
        bool actual = await this.userService.MakeAdmin(invalidId);

        // Assert
        Assert.That(actual, Is.False);
    }

   


}
