namespace RetroWars.Web.App.Areas.Admin.ViewModels;
public class PollAdminViewModel
{
    public Guid Id { get; set; }

    public bool IsActive { get; set; }

    public string FirstGameName { get; set; } = null!;

    public string FirstGamePublisher { get; set; } = null!;

    public string FirstGamePlatform { get; set; } = null!;

   // public Guid SecondGameId { get; set; }

    public string SecondGameName { get; set; } = null!;

    public string SecondGamePublisher { get; set; } = null!;

    public string SecondGamePlatform { get; set; } = null!;

    public int VotesForFirst { get; set; }

    public int VotesForSecond { get; set; }

    public int TotalVotes => this.VotesForFirst + this.VotesForSecond;
}

