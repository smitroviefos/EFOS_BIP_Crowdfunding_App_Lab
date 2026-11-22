using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkillPledge.Models;

namespace SkillPledge.Data;

/// <summary>
/// Creates the database (applies migrations) and fills it with demo data
/// the first time the app runs in Development.
/// </summary>
public static class DbSeeder
{
    // Demo accounts for the workshop only – never use such passwords in a real app
    public const string DemoPassword = "Demo!2026";

    private static readonly string[] SkillNames =
    [
        "Marketing", "Business plan", "Graphic design", "UX design",
        "Web development", "Mobile development", "Data analysis", "Financial planning",
        "Legal advice", "Social media", "Photography & video", "Copywriting"
    ];

    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // Creates the database and all tables if they do not exist yet
        await db.Database.MigrateAsync();

        if (!await db.Skills.AnyAsync())
        {
            db.Skills.AddRange(SkillNames.Select(name => new Skill { Name = name }));
            await db.SaveChangesAsync();
        }

        if (await db.Projects.AnyAsync())
        {
            return; // demo data already exists
        }

        var ana = await EnsureUserAsync(userManager, "ana.demo@skillpledge.test");
        var luka = await EnsureUserAsync(userManager, "luka.demo@skillpledge.test");
        var skills = await db.Skills.ToDictionaryAsync(s => s.Name);

        var game = new Project
        {
            Title = "Pixel Rangers – a 2D platformer game",
            Description = "I am a computer science student building a 2D platformer for PC and mobile. " +
                          "The game works, but I know nothing about promotion, pricing or a business plan.",
            Deadline = DateTime.Today.AddDays(30),
            OwnerId = ana.Id,
            SkillsNeeded = { skills["Marketing"], skills["Business plan"], skills["Graphic design"] }
        };

        var cafe = new Project
        {
            Title = "Zero-waste campus café",
            Description = "Economics students planning a zero-waste café on campus. " +
                          "We have the business plan – we need a website, a booking flow and good photos.",
            Deadline = DateTime.Today.AddDays(45),
            OwnerId = luka.Id,
            SkillsNeeded = { skills["Web development"], skills["UX design"], skills["Photography & video"] }
        };

        var languageApp = new Project
        {
            Title = "Language exchange app for Erasmus students",
            Description = "A mobile app that matches Erasmus students who want to practise each other's languages.",
            Deadline = DateTime.Today.AddDays(60),
            OwnerId = ana.Id,
            SkillsNeeded = { skills["Mobile development"], skills["Legal advice"], skills["Social media"] }
        };

        var farmersMarket = new Project
        {
            Title = "Online market for local farmers",
            Description = "Helping small farmers from Slavonia sell their products online. " +
                          "We need a web shop prototype, a pricing model and a social media campaign.",
            Deadline = DateTime.Today.AddDays(20),
            Status = ProjectStatus.InProgress,
            OwnerId = luka.Id,
            SkillsNeeded = { skills["Web development"], skills["Financial planning"], skills["Social media"] }
        };

        db.Projects.AddRange(game, cafe, languageApp, farmersMarket);
        await db.SaveChangesAsync();

        db.Pledges.AddRange(
            new Pledge
            {
                ProjectId = game.Id, UserId = luka.Id, SkillId = skills["Marketing"].Id, Hours = 10,
                Message = "I can prepare a launch campaign and a simple marketing plan."
            },
            new Pledge
            {
                ProjectId = farmersMarket.Id, UserId = ana.Id, SkillId = skills["Web development"].Id, Hours = 15,
                Message = "I can build the first version of the web shop."
            });
        await db.SaveChangesAsync();
    }

    private static async Task<IdentityUser> EnsureUserAsync(UserManager<IdentityUser> userManager, string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is not null)
        {
            return user;
        }

        user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
        var result = await userManager.CreateAsync(user, DemoPassword);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                "Could not create demo user: " + string.Join("; ", result.Errors.Select(e => e.Description)));
        }

        return user;
    }
}
