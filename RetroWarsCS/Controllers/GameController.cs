﻿namespace RetroWars.Web.Controllers;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Extensions;
using ViewModels.Game;
using RetroWars.Services.Data.Contracts;
using static Common.NotificationMessagesConstants;
using static Common.GeneralApplicationConstants;

public class GameController : AuthorizationController
{
    private readonly IGameService gameService;
    private readonly IGenreService genreService;
    private readonly IPlatformService platformService;
    private readonly IUserService userService;
    private readonly IMemoryCache cache;
    public GameController(IGameService gameService, IGenreService genreService, IPlatformService platformService, IUserService userService, IMemoryCache cache)
    {
        this.userService = userService;
        this.gameService = gameService;
        this.genreService = genreService;
        this.platformService = platformService;
        this.cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        try
        {
            IEnumerable<GameViewModel> allGames =
                this.cache.Get<IEnumerable<GameViewModel>>(GamesCacheKey);

            if (allGames == null)
            {
                allGames = await this.gameService.GetAllGamesAsync();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(GamesCacheDurationMinutes));

                this.cache.Set(GamesCacheKey, allGames, cacheOptions);
            }

           
            return this.View(allGames);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't load games.";
            return this.View(new List<GameViewModel>());
        }
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        try
        {
            GameFormModel formModel = new GameFormModel()
            {
                Platforms = await this.platformService.GetAllPlatformsAsync(),
                Genres = await this.genreService.GetAllGenresAsync()
            };
            return this.View(formModel);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't load platforms or/and categories";
            return this.View(new GameFormModel());
        }

    }

    [HttpPost]
    public async Task<IActionResult> Add(GameFormModel formModel)
    {
        try
        {
            this.cache.Remove(GamesCacheKey);

            if (ModelState.IsValid)
            {
                await this.gameService.CreateGameAsync(formModel);
                TempData[SuccessMessage] = "Success: Game added!";
                return RedirectToAction("All");
            }

            TempData[ErrorMessage] = "Error: Invalid data";

            formModel.Genres = await this.genreService.GetAllGenresAsync();
            formModel.Platforms = await this.platformService.GetAllPlatformsAsync();

            return this.View(formModel);

        }
        catch
        {
            TempData[ErrorMessage] = "Error: Can't add Game";
            formModel.Platforms = await this.platformService.GetAllPlatformsAsync();
            formModel.Genres = await this.genreService.GetAllGenresAsync();
            return this.View(formModel);
        }


    }
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        try
        {
            GameViewModel viewModel = await this.gameService.GetOneGameAsync(id);

            return this.View(viewModel);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Invalid Game Id provided";
            return RedirectToAction("All");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        try
        {
            GameViewModel viewModel = await this.gameService.GetOneGameAsync(id);

            GameFormModel formModel = new GameFormModel()
            {

                Name = viewModel.Name,
                Description = viewModel.Description,
                Developer = viewModel.Developer,
                Publisher = viewModel.Publisher,
                YearOfPublishing = viewModel.YearOfPublishing,
                GenreId = Guid.Parse(viewModel.GenreId),
                PlatformId = Guid.Parse(viewModel.PlatformId),
                Platforms = await this.platformService.GetAllPlatformsAsync(),
                Genres = await this.genreService.GetAllGenresAsync()
            };

            return this.View(formModel);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Invalid Game Id provided";
            return RedirectToAction("All");
        }

    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, GameFormModel formModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await this.gameService.EditGameAsync(id, formModel);
                TempData[SuccessMessage] = "Success: Game edited.";
                return RedirectToAction("All");
            }
            TempData[ErrorMessage] = "Error: Invalid data";

            formModel.Genres = await this.genreService.GetAllGenresAsync();
            formModel.Platforms = await this.platformService.GetAllPlatformsAsync();

            return this.View(formModel);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Couldn't update model.";
            return RedirectToAction("All");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            GameViewModel viewModel = await this.gameService.GetOneGameAsync(id);

            GameFormModel formModel = new GameFormModel()
            {

                Name = viewModel.Name,
                Description = viewModel.Description,
                Developer = viewModel.Developer,
                Publisher = viewModel.Publisher,
                YearOfPublishing = viewModel.YearOfPublishing,
                GenreId = Guid.Parse(viewModel.GenreId),
                PlatformId = Guid.Parse(viewModel.PlatformId),
                Platforms = await this.platformService.GetAllPlatformsAsync(),
                Genres = await this.genreService.GetAllGenresAsync()
            };

            return this.View(formModel);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Invalid Game Id provided";
            return RedirectToAction("All");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id, bool other = true)
    {
        try
        {
            await this.gameService.DeleteGameAsync(id);
            TempData[SuccessMessage] = "Success: Game deleted";
            return RedirectToAction("All");
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Invalid Game Id provided";
            return RedirectToAction("All");
        }
    }

    [HttpGet]
    public async Task<IActionResult> AddToFavorites(string id)
    {
        string userId = User.GetId()!;

        await this.userService.AddGameToFavoritesAsync(id, userId);

        return RedirectToAction("Favorites");
    }

    [HttpGet]
    public async Task<IActionResult> Favorites()
    {
        string userId = User.GetId()!;
        try
        {
            IEnumerable<GameViewModel> viewModel = await this.gameService.GetFavoritesAsync(userId);

            return this.View(viewModel);
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Couldn't get favorite games.";
            return RedirectToAction("All");
        }
    }

    [HttpGet]
    public async Task<IActionResult> RemoveFromFavorites(string id)
    {
        string userId = User.GetId()!;
        try
        {
            await this.userService.RemoveGameFromFavoritesAsync(id, userId);
            return RedirectToAction("Favorites");
        }
        catch
        {
            TempData[ErrorMessage] = "Error: Couldn't remove from favorite games.";
            return RedirectToAction("All");
        }
    }
}

