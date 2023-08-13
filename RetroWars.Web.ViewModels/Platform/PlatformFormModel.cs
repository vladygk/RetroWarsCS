namespace RetroWars.Web.ViewModels.Platform;

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static RetroWars.Common.EntityValidationConstants.Platform;
public class PlatformFormModel
{
    [Required]
    [StringLength(MaxNameLength, MinimumLength =MinNameLength)]
    public string Name { get; set; } = null!;

    [Required]
    public IFormFile? File { get; set; }

    public string ImageUrl { get; set; } = null!;

    [Required]
    [StringLength(MaxCompanyNameLength, MinimumLength =MinCompanyNameLength)]
    public string Company { get; set; } = null!;

    [Required]
    [StringLength(MaxDescriptionLength, MinimumLength = MinDescriptionLength)]
    public string Description { get; set; } = null!;

    [Required]
    [Range(typeof(int),MinYear, MaxYear)]
    public int YearOfRelease { get; set; }

   
}
