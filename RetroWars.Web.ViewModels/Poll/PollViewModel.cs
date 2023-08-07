namespace RetroWars.Web.ViewModels.Poll;

public class PollViewModel
{
    public PollViewModel()
    {
        this.Voters = new HashSet<Guid>();
    }
    public Guid Id { get; set; }

    public bool IsActive { get; set; }

    public Guid FirstGameId { get; set; }

    public string FirstGameName { get; set; } = null!;

    public string FirstGameImageUrl { get; set; } = null!;

    public string FirstGamePublisher{get; set; } = null!;
    public string FirstGamePlatform { get; set; } = null!;

    public Guid SecondGameId { get; set; }

    public string SecondGameName { get; set; } = null!;

    public string SecondGamePublisher { get; set; } = null!;
    public string SecondGamePlatform { get; set; } = null!;

    public string SecondGameImageUrl { get; set; } = null!;

    public int VotesForFirst { get; set; }

    public int VotesForSecond { get; set; }

    public Guid Vote { get; set; }

    public IEnumerable<Guid> Voters { get; set; }
}

