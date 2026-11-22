# SkillPledge – Sprint 2: CRUD and pledges

**SkillPledge** is the BIP Crowdfunding Business Application Lab app: students back projects with their **knowledge and time** instead of money.

This folder is the state of the app at the **end of Coding Sprint 2** (Tuesday, 8 December 2026). It builds on the Sprint 1 version.

| | |
|---|---|
| **Tech stack** | ASP.NET Core MVC · .NET 10 · Entity Framework Core · SQL Server · ASP.NET Core Identity (individual accounts) |
| **Sprint 2 goal** | Full CRUD for projects – then students can offer their knowledge to a project |

---

## ✅ What is new in Sprint 2

**Must have – CRUD for projects**

| | Page | Rules |
|---|---|---|
| **Create** | `/Projects/Create` | Logged-in users only · title, description and at least one skill required · deadline must be in the future |
| **Read** | `/Projects` and `/Projects/Details/{id}` | Everyone can browse |
| **Update** | `/Projects/Edit/{id}` | Only the owner · can also change the status (Open / In progress / Completed) |
| **Delete** | `/Projects/Delete/{id}` | Only the owner · confirmation page · its pledges are deleted too |

**Should have – pledges (our “currency”)**

- **Offer your knowledge** (`/Pledges/Create?projectId={id}`): choose one of the skills the project needs, the number of hours and a message
- Rules: you cannot pledge to your own project, and only **Open** projects accept pledges
- **Withdraw** your own pledge on the project page
- **“3 of 5 skills covered”** progress bar on the projects list and the project page – our version of the funding progress bar

**Could have – done in this version**

- Search projects by title or description and filter them by skill
- Covered skills are marked with ✓
- Success and warning messages after every action

From **Sprint 1**: data model, demo data, Identity (register / login), home page.

No database changes were needed in Sprint 2, so there is **no new migration** – the Sprint 1 database works as it is.

---

## 🧪 Try it in 2 minutes

1. Log in as `ana.demo@skillpledge.test` (password `Demo!2026`).
2. **Post a project**, then **edit** it and change its status.
3. Log out, log in as `luka.demo@skillpledge.test` and **offer your knowledge** to Ana’s project – watch the progress bar grow.
4. **Withdraw** the pledge, then log in as Ana again and **delete** the project.

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
| `ViewModels/` | `ProjectFormViewModel`, `PledgeFormViewModel` – the data of the forms |
| `Data/ApplicationDbContext.cs` | Tables and relationships |
| `Data/DbSeeder.cs` | Creates the database and the demo data |
| `Data/Migrations/` | Database migrations |
| `Controllers/ProjectsController.cs` | List, details, create, edit and delete projects |
| `Controllers/PledgesController.cs` | Offer and withdraw a pledge |
| `Views/Projects/` | Razor views for the projects (`_ProjectForm.cshtml` is shared by Create and Edit) |
| `Views/Pledges/Create.cshtml` | The “Offer your knowledge” form |
| `Views/Shared/_SkillsProgress.cshtml` | The “skills covered” progress bar |
| `Views/Shared/_StatusBadge.cshtml` | Coloured badge for the project status |
| [`PROMPTS.md`](PROMPTS.md) | AI prompts that help with Sprint 2 – add your team’s best prompts here |
