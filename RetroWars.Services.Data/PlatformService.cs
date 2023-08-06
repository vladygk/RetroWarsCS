namespace RetroWars.Services.Data;

using RetroWars.Data.Models;
using Retrowars.Data.Repository;
using Contracts;
using Web.ViewModels.Platform;
public class PlatformService : IPlatformService
{
    private readonly IRepository<Platform> platformRepository;
    public PlatformService(IRepository<Platform> platformRepository)
    {
        this.platformRepository = platformRepository;
    }
    public async Task<IEnumerable<GameSelectPlatformsFormModel>> GetAllPlatformsAsync()
    {
        IEnumerable<Platform> allPlatforms = await this.platformRepository.GetAllAsync();

        IEnumerable<GameSelectPlatformsFormModel> allPlatformsFormMoldes = allPlatforms.Select(p =>
            new GameSelectPlatformsFormModel()
            {
                Id = p.Id,
                Name = p.Name,
            });
        return allPlatformsFormMoldes;
    }
}

