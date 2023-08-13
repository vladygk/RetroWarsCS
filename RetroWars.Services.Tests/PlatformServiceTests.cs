namespace RetroWars.Services.Tests;
using RetroWars.Data.Repository;
using RetroWars.Data.Models;
using RetroWars.Services.Data;
using RetroWars.Services.Data.Contracts;
using RetroWars.Services.Tests.Utils;
using RetroWars.Web.ViewModels.Platform;
using System.Text.Json;
using static Utils.TestingConstants;

[TestFixture]
public class PlatformServiceTests
{
    private IPlatformService platformService;
    private IRepository<Platform> platformMockRepository;
    private IFileUploadService fileUploadService;
    private IFireBaseService fireBaseService;
    private Game game;
    private IRepository<Game> gameMockRepository;
    private Platform platform;
    private PlatformViewModel platformViewModel;
    private GameSelectPlatformsFormModel gameSelectPlatformsFormModel;

    [SetUp]
    public void Setup()
    {
        this.platform = TestObjectsFactory.CreatePlatform();
        this.platformViewModel = TestObjectsFactory.CreatePlatformViewModel();
        this.gameSelectPlatformsFormModel = TestObjectsFactory.CreateGameSelectPlatformsFormModel();
        this.fileUploadService = MocksFactory.CreateMockFileUploadService();
        this.fireBaseService = MocksFactory.CreateMockFirebaseService();
        this.platformMockRepository = MocksFactory.CreateMockRepository<Platform>(this.platform);
        this.game = TestObjectsFactory.CreateGame();
        this.gameMockRepository = MocksFactory.CreateMockRepository<Game>(this.game);

        this.platformService = new PlatformService(this.platformMockRepository, this.fileUploadService, this.fireBaseService, this.gameMockRepository);
    }


    [Test]
    public async Task GetAllPlatformsAsyncWorksCorrectly()
    {
        // Arrange
        IEnumerable<GameSelectPlatformsFormModel> expected = new List<GameSelectPlatformsFormModel>() { this.gameSelectPlatformsFormModel };
        // Act
        IEnumerable<GameSelectPlatformsFormModel> actual = await this.platformService.GetAllPlatformsAsync();
        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        // Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));

    }

    [Test]
    public async Task GetAllPlatformsViewModelAsyncWorksCorrectly()
    {
        // Arrange
        IEnumerable<PlatformViewModel> expected = new List<PlatformViewModel>() { this.platformViewModel };
        // Act
        IEnumerable<PlatformViewModel> actual = await this.platformService.GetAllPlatformsViewModelAsync();
        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);

        // Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }


    [Test]
    public async Task GetOnePlatformsViewModelAsyncWorksCorrectly()
    {
        // Arrange
        PlatformViewModel expected = this.platformViewModel;

        // Act
        PlatformViewModel actual = await this.platformService.GetOnePlatformsViewModelAsync(entityId);
        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);

        // Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }

    [Test]
    public void GetOnePlatformsViewModelAsyncThrowsWithInvalidId()
    {
        Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await this.platformService.GetOnePlatformsViewModelAsync(invalidId);
        }, "Invalid Platform Id");
    }

    
}
