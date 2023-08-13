using Microsoft.AspNetCore.Identity;
using RetroWars.Data.Models;
using RetroWars.Data.Repository;
namespace RetroWars.Services.Data;

using Contracts;
using RetroWars.Web.ViewModels.Game;
using Web.ViewModels.User;
using static Common.GeneralApplicationConstants;


public class UserService : IUserService
{
    private readonly IRepository<ApplicationUser> applicationUserRepository;
    private readonly IRepository<Game> gameRepository;
    private readonly UserManager<ApplicationUser> userManager;
    public UserService(IRepository<ApplicationUser> applicationUserRepository, IRepository<Game> gameRepository, UserManager<ApplicationUser> userManager)
    {
        this.applicationUserRepository = applicationUserRepository;
        this.gameRepository = gameRepository;
        this.userManager = userManager;
    }

    public async Task<string> GetFullNameByEmailAsync(string email)
    {
        ApplicationUser? user = await this.applicationUserRepository
            .GetOneAsync(email, true);

        if (user == null)
        {
            return string.Empty;
        }

        return $"{user.FirstName} {user.LastName}";
    }

    public async Task<bool> AddGameToFavoritesAsync(string gameId, string userId)
    {
        try
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
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveGameFromFavoritesAsync(string gameId, string userId)
    {
        try
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
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<IEnumerable<GameViewModel>> GetApplicationUserFavoritesByIdAsync(string userId)
    {
        ApplicationUser? user = await this.applicationUserRepository.GetOneAsync(userId, false);
        if (user is null)
        {
            throw new ArgumentException("Invalid Id.");
        }

        var result =  user.FavoriteGames.Select(g => new GameViewModel() {
            Id = g.Id.ToString(),
            Name = g.Name,
            Description = g.Description,
            Developer = g.Developer,
            Publisher = g.Publisher,
            YearOfPublishing = g.YearOfPublishing,
            PlatformId = g.PlatformId.ToString(),
            Platform = g.Platform.Name,
            Genre = g.Genre.Name.ToString(),
            GenreId = g.GenreId.ToString(),
            ImageUrl = g.ImageUrl,
        });
        return result;
    }

    public async Task<bool> MakeAdmin(string userId)
    {
        try
        {
            ApplicationUser? user = await this.applicationUserRepository.GetOneAsync(userId, false);

            if (user == null)
            {
                throw new ArgumentException("Invalid Id");
            }
            await userManager.AddToRoleAsync(user, AdminRoleName);
            return true;
        }
        catch
        {
            return false;
        }

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
                FullName = u.FirstName + " " + u.LastName,
                IsAdmin = this.userManager.IsInRoleAsync(u, AdminRoleName).Result
            })
        .ToList();


        return allUsersViewModels;
    }
}
