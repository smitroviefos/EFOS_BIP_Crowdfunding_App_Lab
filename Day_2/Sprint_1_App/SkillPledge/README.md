# SkillPledge – Sprint 1: the foundation

**SkillPledge** is the BIP Crowdfunding Business Application Lab app: students back projects with their **knowledge and time** instead of money.

This folder is the state of the app at the **end of Coding Sprint 1** (Tuesday, 8 December 2026).

| | |
|---|---|
| **Tech stack** | ASP.NET Core MVC · .NET 10 · Entity Framework Core · SQL Server · ASP.NET Core Identity (individual accounts) |
| **Sprint 1 goal** | The app runs on every team computer, talks to the database and shows its first pages |

---

## ✅ What is in Sprint 1

- **Data model** for knowledge crowdfunding: `Project`, `Skill`, `Pledge` (+ the `ProjectSkills` link table)
- **Database** created automatically on the first run, with **demo data**: 12 skills, 4 projects, 2 pledges, 2 demo users
- **Projects list** (`/Projects`) – all projects as cards, newest first, with the skills they need
- **Project details** (`/Projects/Details/{id}`) – description, skills needed and pledges
- **Home page** that explains the concept
- **Register / Login** from the ASP.NET Core Identity template (no e-mail confirmation needed)

Coming in **Sprint 2**: Create, Edit and Delete for projects, pledging a skill, and the “skills covered” progress bar.

---

## 🗂️ Data model

- A **user** (ASP.NET Core Identity) can own many **projects** and make many **pledges**.
- A **project** needs many **skills**, and a skill can be needed by many projects (`ProjectSkills`).
- A **pledge** links one user, one project and one skill, with the number of hours offered.

| Table | Key fields |
|---|---|
| `Projects` | Title, Description, Deadline, Status (Open / InProgress / Completed), CreatedAt, OwnerId |
| `Skills` | Name (unique) |
| `ProjectSkills` | Which skills a project needs (many-to-many) |
| `Pledges` | ProjectId, UserId, SkillId, Hours, Message – a pledge of knowledge instead of money |

---

## ▶️ Run it on an EFOS lab computer (Windows)

The app uses **SQL Server LocalDB**, which comes with Visual Studio – no Docker and no extra installation needed.

1. Open `SkillPledge.slnx` in **Visual Studio 2026**.
2. Press **F5**.
3. The first run creates the database `SkillPledgeDb` and fills it with demo data. Click **Projects** in the menu.

If LocalDB is missing, open the *Visual Studio Installer* → *Modify* → *Individual components* and tick **SQL Server Express LocalDB**.

---

## 🍎 Run it on a Mac (SQL Server in Docker)

LocalDB exists only on Windows, so on a Mac use SQL Server in Docker (see the setup guide in the main repository README).

Point the app to your container with **user secrets**, so your password never ends up on GitHub. In Terminal, inside the project folder (the one with `SkillPledge.csproj`):

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" 'Server=localhost,1433;Database=SkillPledgeDb;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;MultipleActiveResultSets=true'
```

> ⚠️ Keep the **single quotes** around the connection string: in zsh (the macOS Terminal) a `!` inside double quotes causes the error `zsh: no such event`.

Then run the app in Rider (or `dotnet run`). User secrets override `appsettings.json` in Development.

---

## 👤 Demo accounts

| E-mail | Password |
|---|---|
| `ana.demo@skillpledge.test` | `Demo!2026` |
| `luka.demo@skillpledge.test` | `Demo!2026` |

You can also register your own account.

---

## 🛠️ Useful commands

```bash
# Install the EF Core tools once
dotnet tool install --global dotnet-ef

# After changing a model class: create a migration
dotnet ef migrations add <MigrationName> -o Data/Migrations

# Apply migrations manually (the app also does this on start-up in Development)
dotnet ef database update
```

To start again with a clean database, delete `SkillPledgeDb` (SQL Server Object Explorer in Visual Studio, or the Database tool in Rider) and run the app again.

---

## 📁 Where to find things

| Folder / file | What is inside |
|---|---|
| `Models/` | `Project`, `Skill`, `Pledge` |
| `Data/ApplicationDbContext.cs` | Tables and relationships |
| `Data/DbSeeder.cs` | Creates the database and the demo data |
| `Data/Migrations/` | Database migrations |
| `Controllers/ProjectsController.cs` | Projects list and details |
| `Views/Projects/` | Razor views for the projects |
| `Views/Shared/_StatusBadge.cshtml` | Coloured badge for the project status |
| [`PROMPTS.md`](PROMPTS.md) | AI prompts that help with Sprint 1 – add your team’s best prompts here |
