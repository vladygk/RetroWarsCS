namespace RetroWars.Services.Data.Contracts;

using Web.ViewModels.Platform;


public interface IPlatformService
{
    public Task<IEnumerable<GameSelectPlatformsFormModel>> GetAllPlatformsAsync();

    public Task<IEnumerable<PlatformViewModel>> GetAllPlatformsViewModelAsync();

    public Task<PlatformViewModel> GetOnePlatformsViewModelAsync(string id);
}