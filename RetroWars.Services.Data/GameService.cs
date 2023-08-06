namespace RetroWars.Services.Data;

using Bluebean_Backend.Utils.Interfaces;
using RetroWars.Data.Models;
using Retrowars.Data.Repository;
using Contracts;
using Web.ViewModels.Game;
using Microsoft.AspNetCore.Http;

using static Common.GeneralApplicationConstants;

public class GameService : IGameService
{
    private readonly IRepository<Game> gameRepository;
    private readonly IFireBaseService firebaseService;

    public GameService(IRepository<Game> gameRepository, IFireBaseService firebaseService)
    {
        this.gameRepository = gameRepository;
        this.firebaseService = firebaseService;
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
            Platforms = g.Platform.Name
        });

        return allGamesViewModels;
    }

    public async Task<GameViewModel?> GetOneGameAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task CreateGameAsync(GameFormModel formModel)
    {
        try
        {
            string pathAndFileName  = await this.UploadFile(formModel.File);

            if (!String.IsNullOrWhiteSpace(pathAndFileName))
            {

                string base64Image = ConvertToBase64(pathAndFileName);

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
        }
        catch (Exception ex)
        {

            throw new Exception("Can't add game");
        }



    }

    public async Task<bool> EditGameAsync(string id, GameFormModel newData)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteGameAsync(string id)
    {
        throw new NotImplementedException();
    }

    private async Task<string> UploadFile(IFormFile file)
    {
        string path = String.Empty;

        string pathAndFileName = String.Empty;
        try
        {
            if (file.Length > 0)
            {
                string filename =Guid.NewGuid() + Path.GetExtension(file.FileName);
                path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Temp"));
                pathAndFileName = Path.Combine(path, filename);
                using (var filestream = new FileStream(pathAndFileName, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }

                return pathAndFileName;
            }

        }
        catch (Exception)
        {
            throw new Exception("Can't upload file");
        }
        return pathAndFileName;
    }

    private string ConvertToBase64(string path)
    {

        byte[] bytes = File.ReadAllBytes(path);
        string fileBase64 = Convert.ToBase64String(bytes);

        return fileBase64;
    }
}

