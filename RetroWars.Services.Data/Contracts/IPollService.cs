using RetroWars.Web.ViewModels.Poll;

namespace RetroWars.Services.Data.Contracts;

public interface IPollService
{
    public Task<IEnumerable<PollViewModel>> GetAllPollsAsync();

    public Task CreatePollAsync( PollFormModel formData);
}