using Microsoft.AspNetCore.Identity;
using RetroWars.Data.Models;
using Retrowars.Data.Repository;
namespace RetroWars.Services.Data;

using Contracts;
using Web.ViewModels.User;
using static Common.GeneralApplicationConstants;


public class UserService : IUserService
{
    private readonly IRepository<ApplicationUser> applicationUserRepository;
    private readonly  IRepository<Game> gameRepository;
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

    public async Task MakeAdmin(string userId)
    {
      ApplicationUser? user = await  this.applicationUserRepository.GetOneAsync(userId, false);

      if (user == null)
      {
          throw new ArgumentException("Invalid Id");
      }
      await userManager.AddToRoleAsync(user, AdminRoleName);

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
                IsAdmin =  this.userManager.IsInRoleAsync(u, AdminRoleName).Result
            })
        .ToList();


        return allUsersViewModels;
    }
}
