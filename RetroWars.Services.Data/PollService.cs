namespace RetroWars.Services.Data;
using RetroWars.Data.Models;
using Retrowars.Data.Repository;
using Contracts;
using Web.ViewModels.Poll;


public class PollService : IPollService
{
    private readonly IRepository<Poll> pollRepository;
    public PollService(IRepository<Poll> pollRepository)
    {
        this.pollRepository = pollRepository;
    }
    public async Task<IEnumerable<PollViewModel>> GetAllPollsAsync()
    {
        IEnumerable<Poll> allPolls = await this.pollRepository.GetAllAsync();

        IEnumerable<PollViewModel> allPollViewModels = allPolls.Select(p => new PollViewModel()
        {
            Id = p.Id,
            FirstGameId = p.FirstGameId,
            SecondGameId = p.SecondGameId,
            FirstGameName = p.FirstGame.Name,
            SecondGameName = p.SecondGame.Name,
            FirstGameImageUrl = p.FirstGame.ImageUrl,
            SecondGameImageUrl = p.SecondGame.ImageUrl,
            IsActive = p.IsActive,
            VotesForFirst = p.VotesForFirst,
            VotesForSecond = p.VotesForSecond
        });

        return allPollViewModels;
    }

    public async Task CreatePollAsync(PollFormModel formModel)
    {
        Poll newPoll = new Poll()
        {
            FirstGameId = Guid.Parse(formModel.SecondGameId),
            SecondGameId = Guid.Parse(formModel.SecondGameId),
            IsActive = true,
            VotesForFirst = 0,
            VotesForSecond = 0,
        };

        await this.pollRepository.AddAsync(newPoll);
    }
}

