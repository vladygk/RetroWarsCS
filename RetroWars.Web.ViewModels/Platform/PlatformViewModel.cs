namespace RetroWars.Web.ViewModels.Platform;

public class PlatformViewModel
{
   
    public Guid Id { get; set; }

    
    public string Name { get; set; } = null!;

   
    public string ImageUrl { get; set; } = null!;


    public string Company { get; set; } = null!;

   
    public string Description { get; set; } = null!;

 
    public int YearOfRelease { get; set; }

    public  string? Games { get; set; }
}