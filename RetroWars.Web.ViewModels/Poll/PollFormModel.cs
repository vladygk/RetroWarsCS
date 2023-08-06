namespace RetroWars.Web.ViewModels.Poll;

using System.ComponentModel.DataAnnotations;

using Game;


public class PollFormModel
{
    public PollFormModel()
    {
        this.Games = new HashSet<PollSelectGameViewModel>();
    }
    [Required]
    public string FirstGameId { get; set; } = null!;
    [Required]
    
    public string SecondGameId { get; set; } = null!;

    public IEnumerable<PollSelectGameViewModel> Games;
}

