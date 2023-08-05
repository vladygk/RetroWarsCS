namespace RetroWars.Services.Data.Contracts;

using Web.ViewModels.User;

public interface IUserService
{
    public Task<string> GetFullNameByIdAsync(Guid userId);
    public Task<IEnumerable<UserViewModel>> AllAsync();
}