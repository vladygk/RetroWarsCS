namespace RetroWars.Services.Data;

using RetroWars.Data.Models;
using Retrowars.Data.Repository;
using Contracts;
using Web.ViewModels.Genre;

public class GenreService : IGenreService
{
    private readonly IRepository<Genre> genRepository;
    public GenreService(IRepository<Genre> genRepository)
    {
        this.genRepository = genRepository;
    }
    public async Task<IEnumerable<GameSelectGenreFormModel>> GetAllGenresAsync()
    {
        IEnumerable<Genre> allGenres = await this.genRepository.GetAllAsync();

        IEnumerable<GameSelectGenreFormModel> allGenreViewModels = allGenres.Select(g => new GameSelectGenreFormModel()
        {
            Id = g.Id,
            Name = g.Name,
        });

        return allGenreViewModels;
    }
}

