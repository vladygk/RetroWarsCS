namespace RetroWars.Services.Data;

using RetroWars.Data.Models;
using RetroWars.Data.Repository;
using Contracts;
using Web.ViewModels.Genre;

public class GenreService : IGenreService
{
    private readonly IRepository<Genre> genreRepository;
    private readonly IRepository<Game> gameRepository;
    public GenreService(IRepository<Genre> genreRepository, IRepository<Game> gameRepository)
    {
        this.genreRepository = genreRepository;
        this.gameRepository = gameRepository;
    }

    public async Task<bool> CheckIfGenreIsAssociatedWithGames(string id)
    {
        var allGames = await this.gameRepository.GetAllAsync();
        return allGames.Any(g =>  g.GenreId== Guid.Parse(id));
    }

    public async Task<bool> CreateGenreAsync(GenreFormModel model)
    {
        try
        {

            Genre genre = new Genre() { 
            Name = model.Name};

            await this.genreRepository.AddAsync(genre);
            await this.genreRepository.SaveAsync();

            return true;
        }
        catch
        {

            return false;
        }
    }

    public async Task<bool> DeleteGenreAsync(string id)
    {
        try
        {
            await this.genreRepository.DeleteOneAsync(Guid.Parse(id));
            await this.genreRepository.SaveAsync();
            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }

    public async Task<IEnumerable<GameSelectGenreFormModel>> GetAllGenresAsync()
    {
        IEnumerable<Genre> allGenres = await this.genreRepository.GetAllAsync();

        IEnumerable<GameSelectGenreFormModel> allGenreViewModels = allGenres.Select(g => new GameSelectGenreFormModel()
        {
            Id = g.Id,
            Name = g.Name,
        });

        return allGenreViewModels;
    }
}

