using RetroWars.Web.ViewModels.Genre;

namespace RetroWars.Services.Data.Contracts;

public interface IGenreService
{
    public Task<IEnumerable<GameSelectGenreFormModel>> GetAllGenresAsync();
}