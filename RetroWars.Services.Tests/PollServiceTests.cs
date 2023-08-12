using RetroWars.Services.Tests.Utils;

namespace RetroWars.Services.Tests;
using Retrowars.Data.Repository;
using RetroWars.Common.Enums;
using RetroWars.Data.Models;
using RetroWars.Services.Data;
using RetroWars.Services.Data.Contracts;
using RetroWars.Services.Tests.Utils;
using RetroWars.Web.App.Areas.Admin.ViewModels;
using RetroWars.Web.ViewModels.Poll;
using System.Text.Json;
using static TestingConstants;

[TestFixture]
public class PollServiceTests
{

    private IPollService pollService;
    private IRepository<Poll> pollRepository;
    private IRepository<ApplicationUser> applicationUserRepository;

    private PollAdminViewModel pollAdminViewMode;
    private PollViewModel pollViewModel;
    private PollFormModel pollFormModel;
    private ApplicationUser applicationUser;
    private Poll poll;
    [SetUp]
    public void Setup()
    {
        this.poll = TestObjectsFactory.CreatePoll();
        this.pollRepository = MocksFactory.CreateMockRepository<Poll>(this.poll);
        this.applicationUser = TestObjectsFactory.CreateApplicationUser();
        this.applicationUserRepository = MocksFactory.CreateMockRepository<ApplicationUser>(this.applicationUser);
        this.pollViewModel = TestObjectsFactory.CreatePollViewModel(false, false);
        this.pollFormModel = TestObjectsFactory.CreatePollFormModel();
        this.pollAdminViewMode = TestObjectsFactory.CreatePollAdminViewModel();

        this.pollService = new PollService(this.pollRepository,this.applicationUserRepository);
    }


    [Test]
    public async Task GetAllPollsAsyncWorksCorrectly()
    {
        // Arrange
        IEnumerable<PollViewModel> expected = new List<PollViewModel>() {pollViewModel };

        // Act
        IEnumerable<PollViewModel> actual = await this.pollService.GetAllPollsAsync();

        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        // Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }
    [Test]
    public async Task GetOnePollAsyncWorksCorrectly()
    {
        // Arrange
        PollViewModel expected = this.pollViewModel;

        // Act
        PollViewModel actual = await this.pollService.GetOnePollAsync(entityId);

        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        // Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }

    [Test]
    public void GetOnePollAsyncThrowsWithInvalidId()
    {
        Assert.ThrowsAsync<ArgumentException>(async () => {
            await this.pollService.GetOnePollAsync(invalidId);
        }, "Invalid poll id.");
    }

    [Test]
    public async Task CreatePollAsyncWorksCorrectly()
    {
        // Act
        bool actual = await this.pollService.CreatePollAsync(this.pollFormModel);

       
        // Assert
        Assert.That(actual, Is.True);
    }


    [Test]
    public async Task CreatePollAsyncWithInvalidData()
    {
        // Act
        bool actual = await this.pollService.CreatePollAsync(null);


        // Assert
        Assert.That(actual, Is.False);
    }

    [Test]
    public async Task IncreaseVotesWorksCorrectlyWithVoteForFirst()
    {
        // Arrange
        VoteOptions choice = VoteOptions.VoteForFirst;
        var expected = TestObjectsFactory.CreatePollViewModel(true, true);
        // Act
        var actual = await this.pollService.IncreaseVotes(entityId, choice);
        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        // Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }
    [Test]
    public async Task IncreaseVotesWorksCorrectlyWithVoteForSecond()
    {
        // Arrange
        VoteOptions choice = VoteOptions.VoteForSecond;
        var expected = TestObjectsFactory.CreatePollViewModel(true, false);
        // Act
        var actual = await this.pollService.IncreaseVotes(entityId, choice);
        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        // Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }


    [Test]
    public  void IncreaseVotesThrowsWithInvalidData()
    {
        Assert.ThrowsAsync<ArgumentException>(async () => {
            await this.pollService.IncreaseVotes(invalidId, VoteOptions.VoteForSecond);
        }, "Invalid poll id");
    }

    [Test]
    public async Task MarkUserAsVotedWorksCorrectly()
    {
        // Act
        bool actual = await this.pollService.MarkUserAsVoted(entityId, entityId);

        // Assert
        Assert.That(actual, Is.True);
    }

    [Test]
    public async Task MarkUserAsVotedWithIncorrectId()
    {
        // Act
        bool actual = await this.pollService.MarkUserAsVoted(invalidId, invalidId);

        // Assert
        Assert.That(actual, Is.False);
    }


    [Test]
    public  void GetResultsWorksCorrectlyWithZeroVotes() {
        // Arrange
        double[] expected = new double[] { 0, 0 };

        // Act
        double[] actual = this.pollService.GetResults(this.pollViewModel.VotesForFirst, this.pollViewModel.VotesForSecond);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void GetResultsWorksCorrectlyWithNonZeroVotes()
    {
        // Arrange
        double[] expected = new double[] { 1, 0 };

        // Act
        double[] actual = this.pollService.GetResults(1, this.pollViewModel.VotesForSecond);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public async Task GetAllActivePollsAsyncWorksCorrectly()
    {
        // Arange
        IEnumerable<PollViewModel> expected = new List<PollViewModel>() { pollViewModel };

        // Act
        IEnumerable<PollViewModel> actual = await this.pollService.GetAllActivePollsAsync();

        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        // Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }

    [Test]
    public async Task DeactivateAPollWorksCorrectly()
    {
        // Act 
        bool actual = await this.pollService.DeactivateAPoll(entityId);

        // Assert

        Assert.That(actual, Is.True);
    }
    [Test]
    public async Task DeactivateAPollWithInvalidId()
    {
        // Act 
        bool actual = await this.pollService.DeactivateAPoll(invalidId);

        // Assert

        Assert.That(actual, Is.False);
    }

    [Test]
    public async Task ActivateAPollWorksCorrectly()
    {
        // Act 
        bool actual = await this.pollService.ActivateAPoll(entityId);

        // Assert

        Assert.That(actual, Is.True);
    }
    [Test]
    public async Task ActivateAPollWithInvalidId()
    {
        // Act 
        bool actual = await this.pollService.ActivateAPoll(invalidId);

        // Assert

        Assert.That(actual, Is.False);
    }

    [Test]
    public async Task GetAllPollAdminViewModelsWorksCorrectly()
    {
        // Arrange
        IEnumerable<PollAdminViewModel> expected = new List<PollAdminViewModel>() { this.pollAdminViewMode };

        // Act
        IEnumerable<PollAdminViewModel> actual = await this.pollService.GetAllPollAdminViewModels();

        var expectedJson = JsonSerializer.Serialize(expected);
        var actualJson = JsonSerializer.Serialize(actual);
        // Assert
        Assert.That(actualJson, Is.EqualTo(expectedJson));
    }
    [Test]
    public async Task DeletePollWorksCorrectly()
    {
        // Act 
        bool actual = await this.pollService.DeletePoll(entityId);

        // Assert

        Assert.That(actual, Is.True);
    }
    [Test]
    public async Task DeletePollWithInvalidId()
    {
        // Act 
        bool actual = await this.pollService.DeletePoll(invalidId);

        // Assert

        Assert.That(actual, Is.False);
    }
}
