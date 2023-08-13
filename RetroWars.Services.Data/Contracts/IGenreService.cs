using RetroWars.Web.ViewModels.Genre;


namespace RetroWars.Services.Data.Contracts;

public interface IGenreService
{
    public Task<IEnumerable<GameSelectGenreFormModel>> GetAllGenresAsync();

    public Task<bool> CreateGenreAsync(GenreFormModel model);
    public Task<bool> DeleteGenreAsync(string id);
    public Task<bool> CheckIfGenreIsAssociatedWithGames(string id);
}