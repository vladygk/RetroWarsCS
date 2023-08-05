namespace RetroWars.Web.ViewModels.Game;

public class GameViewModel
{

    public string Id { get; set; }

    public string Name { get; set; } = null!;

    public string Developer { get; set; } = null!;

    public string Publisher { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public int YearOfPublishing { get; set; }

    public string Genre { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Platforms { get; set; } = null!;
}

