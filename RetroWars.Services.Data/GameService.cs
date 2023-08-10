namespace RetroWars.Services.Data;

using RetroWars.Data.Models;
using Retrowars.Data.Repository;
using Contracts;
using Web.ViewModels.Game;

using static Common.GeneralApplicationConstants;

public class GameService : IGameService
{
    private readonly IRepository<Game> gameRepository;
    private readonly IFireBaseService firebaseService;
    private readonly IUserService userService;
    private readonly IFileUploadService fileUploadService;
    public GameService(IRepository<Game> gameRepository, IFireBaseService firebaseService, IUserService userService, IFileUploadService fileUploadService)
    {
        this.gameRepository = gameRepository;
        this.firebaseService = firebaseService;
        this.userService = userService;
        this.fileUploadService = fileUploadService;
    }

    public async Task<IEnumerable<GameViewModel>> GetAllGamesAsync()
    {
        IEnumerable<Game> allGames = await this.gameRepository.GetAllAsync();

        IEnumerable<GameViewModel> allGamesViewModels = allGames.Select(g => new GameViewModel()
        {
            Id = g.Id.ToString(),
            Name = g.Name,
            Developer = g.Developer,
            Publisher = g.Publisher,
            YearOfPublishing = g.YearOfPublishing,
            Description = g.Description,
            ImageUrl = g.ImageUrl,
            Genre = g.Genre.Name,
            GenreId = g.GenreId.ToString(),
            Platform = g.Platform.Name,
            PlatformId = g.PlatformId.ToString()
        }).ToList();

        return allGamesViewModels;
    }

    public async Task<GameViewModel?> GetOneGameAsync(string id)
    {
        Game? toFind = await this.gameRepository.GetOneAsync(id, false);
        if (toFind is null)
        {
            throw new ArgumentException("Invalid id");
        }

        GameViewModel viewModel = new GameViewModel()
        {
            Id = toFind.Id.ToString(),
            ImageUrl = toFind.ImageUrl,
            Name = toFind.Name,
            Description = toFind.Description,
            Developer = toFind.Developer,
            Publisher = toFind.Publisher,
            YearOfPublishing = toFind.YearOfPublishing,
            Genre = toFind.Genre.Name,
            GenreId = toFind.GenreId.ToString(),
            Platform = toFind.Platform.Name,
            PlatformId = toFind.PlatformId.ToString()

        };
        return viewModel;
    }

    public async Task<bool> CreateGameAsync(GameFormModel formModel)
    {
        try
        {
            string pathAndFileName = await fileUploadService.UploadFile(formModel.File);

            if (!String.IsNullOrWhiteSpace(pathAndFileName))
            {

                string base64Image = fileUploadService.ConvertToBase64(pathAndFileName);

                string imageUrl = await this.firebaseService.UploadFile(base64Image, DefaultFireBaseStorageFolder, pathAndFileName.Split("\\", StringSplitOptions.RemoveEmptyEntries)[^1]);

                Game newGame = new Game()
                {
                    Name = formModel.Name,
                    Description = formModel.Description,
                    Developer = formModel.Developer,
                    Publisher = formModel.Publisher,
                    YearOfPublishing = formModel.YearOfPublishing,
                    GenreId = formModel.GenreId,
                    PlatformId = formModel.PlatformId,
                    ImageUrl = imageUrl,
                };


                await this.gameRepository.AddAsync(newGame);
                await this.gameRepository.SaveAsync();

                File.Delete(pathAndFileName);
            }
            return true;
        }
        catch (Exception ex)
        {

            return false;
        }



    }

    public async Task<bool> EditGameAsync(string id, GameFormModel newData)
    {

        try
        {
            Game toEdit = await this.gameRepository.GetOneAsync(id, false);

            if (toEdit is null)
            {
                throw new ArgumentException("Invalid Id");
            }

            Game editedGame = new Game()
            {
                Id = Guid.Parse(id),
                Name = newData.Name,
                Description = newData.Description,
                Developer = newData.Developer,
                Publisher = newData.Publisher,
                YearOfPublishing = newData.YearOfPublishing,
                GenreId = newData.GenreId,
                PlatformId = newData.PlatformId,
                ImageUrl = toEdit.ImageUrl,
            };


            await this.gameRepository.UpdateOneAsync(editedGame);
            await this.gameRepository.SaveAsync();
            return true;
        }
        catch
        {
            return false;
        }

    }

    public async Task<bool> DeleteGameAsync(string id)
    {
        try
        {
            await this.gameRepository.DeleteOneAsync(Guid.Parse(id));
            await this.gameRepository.SaveAsync();
            return true;
        }
        catch
        {
            return false;
        }
        }


    public async Task<IEnumerable<GameViewModel>> GetFavoritesAsync(string userId)
    {
       
        IEnumerable<GameViewModel> favoriteGames = await this.userService.GetApplicationUserFavoritesByIdAsync(userId);

      
        return favoriteGames;
    }

    public async Task<IEnumerable<PollSelectGameViewModel>> GetAllPollSelectGameViewModels()
    {
        IEnumerable<Game> allGames = await this.gameRepository.GetAllAsync();

        IEnumerable<PollSelectGameViewModel> models = allGames.Select(g => new PollSelectGameViewModel()
        {
            Id = g.Id.ToString(),
            Name = g.Name,
        });

        return models;
    }

    
}

