using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillPledge.Models;

namespace SkillPledge.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Pledge> Pledges => Set<Pledge>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Keep this call first: it configures the ASP.NET Core Identity tables
        base.OnModelCreating(builder);

        builder.Entity<Skill>()
            .HasIndex(s => s.Name)
            .IsUnique();

        builder.Entity<Project>(project =>
        {
            // Store the status as readable text (Open, InProgress, Completed)
            project.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(20);

            // A user cannot be deleted while they still own projects
            project.HasOne(p => p.Owner)
                .WithMany()
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-many: a project needs many skills, a skill appears in many projects
            project.HasMany(p => p.SkillsNeeded)
                .WithMany(s => s.Projects)
                .UsingEntity(join => join.ToTable("ProjectSkills"));
        });

        builder.Entity<Pledge>(pledge =>
        {
            // Deleting a project also deletes its pledges
            pledge.HasOne(p => p.Project)
                .WithMany(p => p.Pledges)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            pledge.HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            pledge.HasOne(p => p.Skill)
                .WithMany()
                .HasForeignKey(p => p.SkillId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
