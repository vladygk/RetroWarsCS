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

    public async Task<IEnumerable<PlatformViewModel>> GetAllPlatformsViewModelAsync()
    {
        IEnumerable<Platform> allPlatforms = await this.platformRepository.GetAllAsync();

        IEnumerable<PlatformViewModel> allPlatformViewModels = allPlatforms.Select(p => new PlatformViewModel()
        {
            Id = p.Id,
            Name = p.Name,
            Company = p.Company,
            ImageUrl = p.ImageUrl,
            Description = p.Description,
            YearOfRelease = p.YearOfRelease,
            Games = String.Join(", ", p.Games.Select(x => x.Name))

        });

        return allPlatformViewModels;
    }

    public async Task<PlatformViewModel> GetOnePlatformsViewModelAsync(string id)
    {
        Platform? platform = await this.platformRepository.GetOneAsync(id, false);

        if (platform is null)
        {
            throw new ArgumentException("Invalid Platform Id"); 
        }

        PlatformViewModel viewModel = new PlatformViewModel()
        {
            Id = platform.Id,
            Name = platform.Name,
            Company = platform.Company,
            ImageUrl = platform.ImageUrl,
            Description = platform.Description,
            YearOfRelease = platform.YearOfRelease,
            Games = String.Join(", ", platform.Games.Select(x => x.Name))
        };

        return viewModel;
    }
}

