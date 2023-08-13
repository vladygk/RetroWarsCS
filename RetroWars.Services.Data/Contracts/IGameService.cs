namespace RetroWars.Services.Data.Contracts;


using Web.ViewModels.Game;

public interface IGameService
{
    public Task<IEnumerable<GameViewModel>> GetAllGamesAsync();
    public Task<GameViewModel?> GetOneGameAsync(string id);

    public Task<bool> CreateGameAsync(GameFormModel gameToAdd);

    public Task<bool> EditGameAsync(string id, GameFormModel newData);

    public Task<bool> DeleteGameAsync(string id);

    public Task<IEnumerable<GameViewModel>> GetFavoritesAsync(string userId);

    public Task<IEnumerable<PollSelectGameViewModel>> GetAllPollSelectGameViewModels();

    public GameFormModel ConvertGameViewModelToFormModel(GameViewModel viewModel);

    public Task<IEnumerable<GameViewModel>> SearchGameByName(string query);
}