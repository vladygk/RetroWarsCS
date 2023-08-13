namespace RetroWars.Services.Tests;

using RetroWars.Data.Repository;
using RetroWars.Data.Models;
using RetroWars.Services.Data;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.ViewModels.Game;
using RetroWars.Web.ViewModels.User;
using static RetroWars.Services.Tests.Utils.TestingConstants;
using System.Text.Json;
using RetroWars.Services.Tests.Utils;

[TestFixture]
public class GameServiceTests
{

    private IRepository<Game> gameRepositoryMock;
    private IFireBaseService fireBaseMock;
    private IUserService userServiceMock;
    private IGameService gameService;
    private IFileUploadService fileUploadServiceMock;
    private GameViewModel gameViewModel;
    private GameFormModel gameFormModel;
    private Game game;
    private UserViewModel user;
    private PollSelectGameViewModel pollSelectGameViewMode;

    [SetUp]
    public void Setup()
    {
        this.game = TestObjectsFactory.CreateGame();
        this.gameViewModel = TestObjectsFactory.CreateGameViewModel();
        this.user = TestObjectsFactory.CreateUserViewModel();
        this.gameFormModel = TestObjectsFactory.CreateGameFormModel();
        this.pollSelectGameViewMode = TestObjectsFactory.CreatePollSelectGameViewModel();


        this.gameRepositoryMock = MocksFactory.CreateMockRepository<Game>( game);
        this.fireBaseMock = MocksFactory.CreateMockFirebaseService();
        this.userServiceMock = MocksFactory.CreateMockUserService( user,  gameViewModel);
        this.fileUploadServiceMock = MocksFactory.CreateMockFileUploadService();
        this.gameService = new GameService(this.gameRepositoryMock, this.fireBaseMock, this.userServiceMock, this.fileUploadServiceMock);

    }


    [Test]
    public async Task GetAllGamesAsyncWorksCorrectly()
    {
        //Arange
        var expected = new List<GameViewModel>(){ gameViewModel };
       
        // Act
        var actual = await this.gameService.GetAllGamesAsync();

        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        //Assert

        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }

    [Test]
    public async Task GetOneGameAsyncWorksCorrectly()
    {
        //Arrange
        var expected = gameViewModel;

        //Act
        var actual = await this.gameService.GetOneGameAsync(entityId);

        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        //Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));

    }

    [Test]
    public async Task GetOneGameAsyncThrowsExceptionWithInvalidId()
    {

        Assert.ThrowsAsync<ArgumentException>(async() => {
            await this.gameService.GetOneGameAsync(invalidId);
        }, "Invalid id");
    }

    [Test]
    public async Task CreateGameAsyncWorksCorrectly()
    {
        // Arrange

        // Act
        bool actual = await this.gameService.CreateGameAsync(this.gameFormModel);

        // Assert
            Assert.That(actual, Is.True);
    }

    [Test]
    public async Task EditGameAsyncWorksCorrectly()
    {
        // Arrange

        // Act
        bool actual = await this.gameService.EditGameAsync(entityId,this.gameFormModel);

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public async Task DeleteGameAsyncWorksCorrectly()
    {
        // Arrange

        // Act
        bool actual = await this.gameService.DeleteGameAsync(entityId);

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public async Task GetFavoritesAsyncWorksCorrectly()
    {
        //Arrange
        var expected = new List<GameViewModel>(){ gameViewModel};
        
        //Act
        var actual = await this.gameService.GetFavoritesAsync(entityId);
        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        //Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));

    }

    [Test]
    public async Task GetAllPollSelectGameViewModelsWorksCorrectly()
    {
        //Arrange
        var expected = new List<PollSelectGameViewModel>() { pollSelectGameViewMode };

        //Act
        var actual = await this.gameService.GetAllPollSelectGameViewModels();
        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        //Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));

    }
}