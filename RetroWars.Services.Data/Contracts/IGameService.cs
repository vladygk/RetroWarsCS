namespace RetroWars.Services.Data.Contracts;


using Web.ViewModels.Game;

public interface IGameService
{
    public Task<IEnumerable<GameViewModel>> GetAllGamesAsync();
    public Task<GameViewModel?> GetOneGameAsync(string id);

    public Task CreateGameAsync(GameFormModel gameToAdd);

    public Task EditGameAsync(string id, GameFormModel newData);

    public Task DeleteGameAsync(string id);
}