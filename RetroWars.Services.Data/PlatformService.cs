namespace RetroWars.Services.Data;

using RetroWars.Data.Models;
using RetroWars.Data.Repository;
using Contracts;
using Web.ViewModels.Platform;
using static Common.GeneralApplicationConstants;
public class PlatformService : IPlatformService
{
    private readonly IRepository<Platform> platformRepository;
    private readonly IRepository<Game> gameRepository;
    private readonly IFileUploadService fileUploadService;
    private readonly IFireBaseService fireBaseService;
    public PlatformService(IRepository<Platform> platformRepository, IFileUploadService fileUploadService, IFireBaseService fireBaseService, IRepository<Game> gameRepository)
    {
        this.platformRepository = platformRepository;
        this.fileUploadService = fileUploadService;
        this.fireBaseService = fireBaseService;
        this.gameRepository = gameRepository;
    }

    public async Task<bool> CreatePlatformAsync(PlatformFormModel model)
    {
        try
        {
            Platform platform = new Platform()
            {
                Name = model.Name,
                Company = model.Company,
                Description = model.Description,
                YearOfRelease = model.YearOfRelease,

            };

            string path = await this.fileUploadService.UploadFile(model.File);

            string base64 = this.fileUploadService.ConvertToBase64(path);

            string url = await this.fireBaseService.UploadFile(base64, DefaultFireBaseStorageFolder, path.Split("\\", StringSplitOptions.RemoveEmptyEntries)[^1]);
            platform.ImageUrl = url;

            await this.platformRepository.AddAsync(platform);
            await this.platformRepository.SaveAsync();

            File.Delete(path);
            return true;
        }
        catch
        {

            return false;
        }
    }

    public async Task<bool> DeletePlatformAsync(string id)
    {
        try
        {
            PlatformViewModel? toDelete = await this.GetOnePlatformsViewModelAsync(id);
            if (toDelete is null)
            {

                throw new ArgumentException("Invalid id,");
            }

            await this.platformRepository.DeleteOneAsync(Guid.Parse(id));
            await this.platformRepository.SaveAsync();
            return true;

        }
        catch
        {

            return false;
        }
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

    public async Task<bool> CheckIfPlatformIsAssociatedWithGames(string id)
    {
       var allGames =  await this.gameRepository.GetAllAsync();
       return allGames.Any(g=>g.Platform.Id == Guid.Parse(id));
    }
}

