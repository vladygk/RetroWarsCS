
namespace RetroWars.Services.Data.Contracts;


using Web.App.Areas.Admin.ViewModels;
using Web.ViewModels.Poll;

using Common.Enums;

public interface IPollService
{
    public Task<IEnumerable<PollViewModel>> GetAllPollsAsync();

    public Task<PollViewModel> GetOnePollAsync(string id);

    public Task<bool> CreatePollAsync( PollFormModel formData);

    public Task<PollViewModel> IncreaseVotes(string id, VoteOptions choice);

    public Task<bool> MarkUserAsVoted(string id, string userId);

    public double[] GetResults(int votesForFirst, int votesForSecond);

    public Task<IEnumerable<PollViewModel>> GetAllActivePollsAsync();
    public Task<bool> DeactivateAPoll(string id);

    public Task<bool> ActivateAPoll(string id);

    public Task<IEnumerable<PollAdminViewModel>> GetAllPollAdminViewModels();

    public Task<bool> DeletePoll(string id);
}