using RetroWars.Data.Models;

namespace RetroWars.Services.Data.Contracts;

using RetroWars.Web.ViewModels.Game;
using Web.ViewModels.User;

public interface IUserService
{
    public Task<string> GetFullNameByIdAsync(string userId);
    public Task<IEnumerable<UserViewModel>> AllAsync();
    public Task<string> GetFullNameByEmailAsync(string email);
    public Task<bool> AddGameToFavoritesAsync(string gameId, string userId);

    public Task<bool> RemoveGameFromFavoritesAsync(string gameId, string userId);
    public Task<IEnumerable<GameViewModel>> GetApplicationUserFavoritesByIdAsync(string userId);

    public Task<bool> MakeAdmin(string userId);
}