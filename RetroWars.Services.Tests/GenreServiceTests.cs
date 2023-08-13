

using RetroWars.Data.Repository;
using RetroWars.Data.Models;
using RetroWars.Services.Data;
using RetroWars.Services.Data.Contracts;
using RetroWars.Services.Tests.Utils;
using RetroWars.Web.ViewModels.Genre;
using System.Text.Json;

namespace RetroWars.Services.Tests;

[TestFixture]
public class GenreServiceTests
{
    private IGenreService genreService;
    private IRepository<Genre> genreMockRepository;
    private Genre genre;
    private GameSelectGenreFormModel gameSelectGenreFormModel;
    [SetUp]
    public void Setup()
    {
        this.genre = TestObjectsFactory.CreateGenre();

        this.genreMockRepository = MocksFactory.CreateMockRepository<Genre>(this.genre);
        this.genreService = new GenreService(this.genreMockRepository);
        this.gameSelectGenreFormModel = TestObjectsFactory.CreateGameSelectGenreFormModel();


    }

    [Test]
    public async Task GetAllGenresAsyncWorksCorrectly()
    {
        // Arrange
        IEnumerable<GameSelectGenreFormModel> expected = new List<GameSelectGenreFormModel>() { this.gameSelectGenreFormModel };
        // Act
        IEnumerable<GameSelectGenreFormModel> actual = await genreService.GetAllGenresAsync();

        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        // Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));

    }


        
    }
