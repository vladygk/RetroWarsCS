﻿
namespace RetroWars.Services.Data;
using RetroWars.Web.App.Areas.Admin.ViewModels;
using Common.Enums;
using RetroWars.Data.Models;
using RetroWars.Data.Repository;
using Contracts;
using Web.ViewModels.Poll;

public class PollService : IPollService
{
    private readonly IRepository<Poll> pollRepository;
    private readonly  IRepository<ApplicationUser> applicationUserRepository;
    public PollService(IRepository<Poll> pollRepository, IRepository<ApplicationUser> applicationUserRepository)
    {
        this.pollRepository = pollRepository;
        this.applicationUserRepository = applicationUserRepository;
    }
    public async Task<IEnumerable<PollViewModel>> GetAllPollsAsync()
    {
        IEnumerable<Poll> allPolls = await this.pollRepository.GetAllAsync();

        IEnumerable<PollViewModel> allPollViewModels = allPolls.Select(p => new PollViewModel()
        {
            Id = p.Id,
            FirstGameId = p.FirstGameId,
            SecondGameId = p.SecondGameId,
            FirstGameName = p.FirstGame.Name,
            SecondGameName = p.SecondGame.Name,
            FirstGameImageUrl = p.FirstGame.ImageUrl,
            SecondGameImageUrl = p.SecondGame.ImageUrl,
            FirstGamePlatform = p.FirstGame.Platform.Name,
            FirstGamePublisher = p.FirstGame.Publisher,
            SecondGamePlatform = p.SecondGame.Platform.Name,
            SecondGamePublisher = p.SecondGame.Publisher,
            IsActive = p.IsActive,
            VotesForFirst = p.VotesForFirst,
            VotesForSecond = p.VotesForSecond,
            Voters = p.Voters.Select(v => v.Id)
        });

        return allPollViewModels;
    }

    public async Task<IEnumerable<PollViewModel>> GetAllActivePollsAsync()
    {
        IEnumerable<Poll> allPolls = await this.pollRepository.GetAllAsync();

        IEnumerable<PollViewModel> allPollViewModels = allPolls.Where(p => p.IsActive).Select(p => new PollViewModel()
        {
            Id = p.Id,
            FirstGameId = p.FirstGameId,
            SecondGameId = p.SecondGameId,
            FirstGameName = p.FirstGame.Name,
            SecondGameName = p.SecondGame.Name,
            FirstGameImageUrl = p.FirstGame.ImageUrl,
            SecondGameImageUrl = p.SecondGame.ImageUrl,
            FirstGamePlatform = p.FirstGame.Platform.Name,
            FirstGamePublisher = p.FirstGame.Publisher,
            SecondGamePlatform = p.SecondGame.Platform.Name,
            SecondGamePublisher = p.SecondGame.Publisher,
            IsActive = p.IsActive,
            VotesForFirst = p.VotesForFirst,
            VotesForSecond = p.VotesForSecond,
            Voters = p.Voters.Select(v => v.Id)
        });

        return allPollViewModels;
    }

    public async Task<PollViewModel> GetOnePollAsync(string id)
    {
        Poll? poll = await this.pollRepository.GetOneAsync(id, false);

        if (poll is null)
        {
            throw new ArgumentException("Invalid poll id.");
        }

        PollViewModel model = new PollViewModel()
        {

            Id = poll.Id,
            FirstGameId = poll.FirstGameId,
            SecondGameId = poll.SecondGameId,
            FirstGameName = poll.FirstGame.Name,
            SecondGameName = poll.SecondGame.Name,
            FirstGameImageUrl = poll.FirstGame.ImageUrl,
            SecondGameImageUrl = poll.SecondGame.ImageUrl,
            FirstGamePlatform = poll.FirstGame.Platform.Name,
            FirstGamePublisher = poll.FirstGame.Publisher,
            SecondGamePlatform = poll.SecondGame.Platform.Name,
            SecondGamePublisher = poll.SecondGame.Publisher,
            IsActive = poll.IsActive,
            VotesForFirst = poll.VotesForFirst,
            VotesForSecond = poll.VotesForSecond,
            Voters = poll.Voters.Select(v => v.Id)
        };

        return model;
    }

    public async Task<bool> CreatePollAsync(PollFormModel formModel)
    {
        try
        {
            Poll newPoll = new Poll()
            {
                FirstGameId = Guid.Parse(formModel.FirstGameId),
                SecondGameId = Guid.Parse(formModel.SecondGameId),
                IsActive = true,
                VotesForFirst = 0,
                VotesForSecond = 0,
            };

            await this.pollRepository.AddAsync(newPoll);
            await this.pollRepository.SaveAsync();

            return true;
        }
        catch
        {

            return false;
        }
    }



    public async Task<PollViewModel> IncreaseVotes(string id, VoteOptions choice)
    {
        Poll? poll = await this.pollRepository.GetOneAsync(id, false);

        if (poll is null)
        {
            throw new ArgumentException("Invalid poll id");
        }

        if (choice == VoteOptions.VoteForFirst)
        {
            poll.VotesForFirst++;
           await  this.pollRepository.SaveAsync();
           return new PollViewModel()
           {
               Id = poll.Id,
               FirstGameId = poll.FirstGameId,
               SecondGameId = poll.SecondGameId,
               FirstGameName = poll.FirstGame.Name,
               SecondGameName = poll.SecondGame.Name,
               FirstGameImageUrl = poll.FirstGame.ImageUrl,
               SecondGameImageUrl = poll.SecondGame.ImageUrl,
               FirstGamePlatform = poll.FirstGame.Platform.Name,
               FirstGamePublisher = poll.FirstGame.Publisher,
               SecondGamePlatform = poll.SecondGame.Platform.Name,
               SecondGamePublisher = poll.SecondGame.Publisher,
               IsActive = poll.IsActive,
               VotesForFirst = poll.VotesForFirst,
               VotesForSecond = poll.VotesForSecond,
               Voters = poll.Voters.Select(v=>v.Id)
           };
        }
        poll.VotesForSecond++;
        await this.pollRepository.SaveAsync();
        return new PollViewModel()
        {
            Id = poll.Id,
            FirstGameId = poll.FirstGameId,
            SecondGameId = poll.SecondGameId,
            FirstGameName = poll.FirstGame.Name,
            SecondGameName = poll.SecondGame.Name,
            FirstGameImageUrl = poll.FirstGame.ImageUrl,
            SecondGameImageUrl = poll.SecondGame.ImageUrl,
            FirstGamePlatform = poll.FirstGame.Platform.Name,
            FirstGamePublisher = poll.FirstGame.Publisher,
            SecondGamePlatform = poll.SecondGame.Platform.Name,
            SecondGamePublisher = poll.SecondGame.Publisher,
            IsActive = poll.IsActive,
            VotesForFirst = poll.VotesForFirst,
            VotesForSecond = poll.VotesForSecond,
            Voters = poll.Voters.Select(v => v.Id)
        };

    }

    public async Task<bool> MarkUserAsVoted(string id, string userId)
    {
        try
        {
            Poll? poll = await this.pollRepository.GetOneAsync(id, false);
            ApplicationUser? user = await this.applicationUserRepository.GetOneAsync(userId, false);


            if (poll is null || user is null)
            {
                throw new ArgumentException("Invalid poll or user id.");
            }
            poll.Voters.Add(user);
            await this.pollRepository.SaveAsync();
            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }

    public double[] GetResults(int votesForFirst, int votesForSecond)
    {
        double[] result = new double[2];
        if(votesForFirst  == 0 && votesForSecond == 0) {
            return result;
        }
        result[0] = ((double)votesForFirst/(votesForFirst+votesForSecond))*100;
        result[1] = ((double)votesForSecond/(votesForSecond+votesForFirst))*100;

        return result;
    }


    
    public async Task<bool> DeactivateAPoll(string id)
    {
        try
        {
            Poll? poll = await this.pollRepository.GetOneAsync(id, false);

            if (poll is null)
            {
                throw new ArgumentException("Invalid id");
            }

            poll.IsActive = false;
            await this.pollRepository.SaveAsync();
            return true;
        }
        catch 
        {

            return false;
        }
    }
      
    public async Task<bool> ActivateAPoll(string id)
    {
        try
        {
            Poll? poll = await this.pollRepository.GetOneAsync(id, false);
           
            if (poll is null)
            {
                throw new ArgumentException("Invalid id");
            }

            poll.IsActive = true;
            await this.pollRepository.SaveAsync();
            return true;
        }
        catch 
        {
            return false;
        }
    }

    public async Task<IEnumerable<PollAdminViewModel>> GetAllPollAdminViewModels()
    {
        IEnumerable<Poll> allPolls = await this.pollRepository.GetAllAsync();

        IEnumerable<PollAdminViewModel> allPollViewModels = allPolls.Select(p => new PollAdminViewModel()
        {
            Id = p.Id,
           
            FirstGameName = p.FirstGame.Name,
            SecondGameName = p.SecondGame.Name,
            FirstGamePlatform = p.FirstGame.Platform.Name,
            FirstGamePublisher = p.FirstGame.Publisher,
            SecondGamePlatform = p.SecondGame.Platform.Name,
            SecondGamePublisher = p.SecondGame.Publisher,
            IsActive = p.IsActive,
            VotesForFirst = p.VotesForFirst,
            VotesForSecond = p.VotesForSecond,
            
        });

        return allPollViewModels;
    }

    public async Task<bool> DeletePoll(string id)
    {
        try
        {
            Poll? toDelete = await this.pollRepository.GetOneAsync(id,false);
            if(toDelete is null)
            {
                throw new ArgumentException("Invalid id.");
            }
            await this.pollRepository.DeleteOneAsync(Guid.Parse(id));

            
            await this.pollRepository.SaveAsync();
            return true;
        }
        catch 
        {

          return false;
        }
    }
}

