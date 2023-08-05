namespace RetroWars.Web.ViewModels.User;

using System.ComponentModel.DataAnnotations;

public class LoginFormModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public string? ReturnUrl { get; set; }
}

