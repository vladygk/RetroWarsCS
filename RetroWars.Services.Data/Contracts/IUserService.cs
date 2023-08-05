namespace RetroWars.Services.Data.Contracts;

using Web.ViewModels.User;

public interface IUserService
{
    public Task<string> GetFullNameByIdAsync(string userId);
    public Task<IEnumerable<UserViewModel>> AllAsync();
    public Task<string> GetFullNameByEmailAsync(string email);
}