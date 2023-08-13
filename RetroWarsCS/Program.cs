namespace RetroWars.Web.App;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetroWars.Data.Repository;
using Data;
using Data.Models;
using RetroWars.Services.Data.Contracts;
using Infrastructure.Extensions;
using static Common.GeneralApplicationConstants;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<RetroWarsDbContext>(options =>
            options.UseSqlServer(connectionString)
                .UseLazyLoadingProxies());

        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); //Add repository to IoC

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount =
                    builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                options.Password.RequireLowercase =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequireUppercase =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequireNonAlphanumeric =
                    builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequiredLength =
                    builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");

            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<RetroWarsDbContext>();


        builder.Services.AddApplicationServices(typeof(IUserService));
        builder.Services.AddRecaptchaService();

        builder.Services.AddMemoryCache();
        builder.Services.AddResponseCaching();

        builder.Services.ConfigureApplicationCookie(cfg =>
        {
            cfg.LoginPath = "/User/Login";
            cfg.AccessDeniedPath = "/Home/Error/401";
        });



        builder.Services.AddControllersWithViews()
            .AddMvcOptions(
            opt =>
            {
                opt.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error/500");
            app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");

            app.UseHsts();
        }



        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseResponseCaching();

        app.UseAuthentication();
        app.UseAuthorization();

        app.SeedAdministrator(AdminEmail);

        app.UseEndpoints(config =>
        {
            config.MapControllerRoute(
                name: "areas",
                pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
                
            );

            config.MapControllerRoute(
                name: "ProtectingUrlRoute",
                pattern: "/{controller}/{action}/{id}/{information}"
                // defaults: new { Controller = "Category", Action = "Details" }
                );


            config.MapDefaultControllerRoute();

            config.MapRazorPages();
        });

        app.Run();
    }
}
