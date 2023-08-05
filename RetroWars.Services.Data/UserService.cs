using RetroWars.Data.Models;
using Retrowars.Data.Repository;
using RetroWars.Services.Data.Contracts;
using RetroWars.Web.ViewModels.User;

namespace RetroWars.Services.Data;

public class UserService : IUserService
{
    private readonly IRepository<ApplicationUser> applicationUserRepository;

    public UserService(IRepository<ApplicationUser> applicationUserRepository)
    {
        this.applicationUserRepository = applicationUserRepository;
    }


    public async Task<string> GetFullNameByIdAsync(Guid userId)
    {
        ApplicationUser? user = await this.applicationUserRepository.GetOneAsync(userId);
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
