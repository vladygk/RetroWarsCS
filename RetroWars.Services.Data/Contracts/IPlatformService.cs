using RetroWars.Web.ViewModels.Platform;

namespace RetroWars.Services.Data.Contracts;

public interface IPlatformService
{
    public Task<IEnumerable<GameSelectPlatformsFormModel>> GetAllPlatformsAsync();
}