using RetroWars.Data.Models;
using Retrowars.Data.Repository;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.ViewModels.User;

namespace RetroWars.Services.Data;

public class UserService : IUserService
{
    private readonly IRepository<ApplicationUser> applicationUserRepository;
    private readonly  IRepository<Game> gameRepository;
    public UserService(IRepository<ApplicationUser> applicationUserRepository, IRepository<Game> gameRepository)
    {
        this.applicationUserRepository = applicationUserRepository;
        this.gameRepository = gameRepository;
    }

    public async Task<string> GetFullNameByEmailAsync(string email)
    {
        ApplicationUser? user = await this.applicationUserRepository
            .GetOneAsync(email,true);

        if (user == null)
        {
            return string.Empty;
        }

        return $"{user.FirstName} {user.LastName}";
    }

    public async Task AddGameToFavoritesAsync(string gameId, string userId)
    {
        
        ApplicationUser? user = await this.applicationUserRepository.GetOneAsync(userId, false);
        Game? game = await this.gameRepository.GetOneAsync(gameId, false);

        if (game is null || user is null)
        {
            throw new ArgumentException("Invalid Ids.");
        }

        user.FavoriteGames.Add(game);
        game.Users.Add(user);

        await this.gameRepository.SaveAsync();

    }

    public async Task RemoveGameFromFavoritesAsync(string gameId, string userId)
    {
        ApplicationUser? user = await this.applicationUserRepository.GetOneAsync(userId, false);
        Game? game = await this.gameRepository.GetOneAsync(gameId, false);

        if (game is null || user is null)
        {
            throw new ArgumentException("Invalid Ids.");
        }
        user.FavoriteGames.Remove(game);
        game.Users.Remove(user);

        await this.gameRepository.SaveAsync();
    }

    public async Task<ICollection<Game>> GetApplicationUserFavoritesByIdAsync(string userId)
    {
        ApplicationUser? user = await this.applicationUserRepository.GetOneAsync(userId, false);
        if (user is null)
        {
            throw new ArgumentException("Invalid Id");
        }
        return user.FavoriteGames;
    }

    public async Task<string> GetFullNameByIdAsync(string userId)
    {
        ApplicationUser? user = await this.applicationUserRepository.GetOneAsync(userId, false);
        if (user == null)
        {
            return string.Empty;
        }

        return $"{user.FirstName} {user.LastName}";
    }

    public async Task<IEnumerable<UserViewModel>> AllAsync()
    {
        IEnumerable<ApplicationUser> allUsers = await this.applicationUserRepository
            .GetAllAsync();

        IEnumerable<UserViewModel> allUsersViewModels =
            allUsers.Select(u => new UserViewModel()
            {
                Id = u.Id.ToString(),
                Email = u.Email,
                FullName = u.FirstName + " " + u.LastName
            })
        .ToList();


        return allUsersViewModels;
    }
}
