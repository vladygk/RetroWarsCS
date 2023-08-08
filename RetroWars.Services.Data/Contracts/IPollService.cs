
namespace RetroWars.Services.Data.Contracts;


using Web.App.Areas.Admin.ViewModels;
using Web.ViewModels.Poll;

using Common.Enums;

public interface IPollService
{
    public Task<IEnumerable<PollViewModel>> GetAllPollsAsync();

    public Task<PollViewModel> GetOnePollAsync(string id);

    public Task CreatePollAsync( PollFormModel formData);

    public Task<PollViewModel> IncreaseVotes(string id, VoteOptions choice);

    public Task MarkUserAsVoted(string id, string userId);

    public double[] GetResults(int votesForFirst, int votesForSecond);

    public Task<IEnumerable<PollViewModel>> GetAllActivePollsAsync();
    public Task DeactivateAPoll(string id);

    public Task ActivateAPoll(string id);

    public Task<IEnumerable<PollAdminViewModel>> GetAllPollAdminViewModels();
}