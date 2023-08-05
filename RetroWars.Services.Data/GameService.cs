using RetroWars.Data.Models;
using Retrowars.Data.Repository;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.ViewModels.Game;

namespace RetroWars.Services.Data;

public class GameService : IGameService
{
    private readonly IRepository<Game> gameRepository;
    public GameService(IRepository<Game> gameRepository)
    {
        this.gameRepository = gameRepository;
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
            Platforms = String.Join(", ", g.Platforms)
        });

        return allGamesViewModels;
    }

    public async Task<GameViewModel?> GetOneGameAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task CreateGameAsync(GameFormModel gameToAdd)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> EditGameAsync(string id, GameFormModel newData)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteGameAsync(string id)
    {
        throw new NotImplementedException();
    }
}

