# SkillPledge – Sprint 3: final version

**SkillPledge** is the BIP Crowdfunding Business Application Lab app: students back projects with their **knowledge and time** instead of money.

This folder is the state of the app at the **end of Coding Sprint 3** (Thursday, 10 December 2026) – the version for the final pitch. It builds on the Sprint 2 version.

| | |
|---|---|
| **Tech stack** | ASP.NET Core MVC · .NET 10 · Entity Framework Core · SQL Server · ASP.NET Core Identity (individual accounts) |
| **Sprint 3 goal** | Code finalization, debugging and quality assurance (QA) testing – a demo-ready app |

---

## ✅ What is new in Sprint 3

**QA – bugs found and fixed**

- No pledges after the project **deadline** (the button is hidden and the server checks it too)
- No **duplicate pledges**: one student can offer a skill to a project only once
- When editing, the deadline cannot be moved into the **past**
- Friendly **“Page not found”** page instead of a bare browser error, and a friendlier error page

**Refactoring**

- The project rules (deadline passed, days left, skills covered, total hours) are now in **one place**: `Models/ProjectRules.cs`
- Repeated view code moved to partial views: `_SkillChips`, `_DeadlineInfo`, `_SkillsProgress`

**Finishing touches for the pitch**

- **My SkillPledge** (`/Dashboard`): my projects with their progress, the knowledge I offered and my hour totals
- **Home page** with live numbers (projects, open projects, pledges, hours) and the newest open projects
- **“12 days left”** on every project (orange in the last week, red when the deadline has passed)
- **Sort** the projects list: newest, deadline soonest, most pledges
- **[TESTING.md](TESTING.md)**: QA checklist with 30+ test cases and a bug report template

From **Sprint 1 and 2**: data model, demo data, Identity, CRUD for projects, pledges, the skills-covered progress bar, search and filter.

No database changes were needed in Sprint 3, so there is **no new migration** – the Sprint 1 database works as it is.

---

## 🧪 Try it in 3 minutes

1. Open the **home page** – look at the live numbers.
2. Log in as `ana.demo@skillpledge.test` (password `Demo!2026`) and **post a project**.
3. Log in as `luka.demo@skillpledge.test` in a private window and **offer your knowledge** to Ana’s project. Try the same skill again – you get an error.
4. Open **My SkillPledge** as Luka and **withdraw** the pledge.
5. Open `/Projects/Details/99999` – the friendly “Page not found” page.

For the full QA test, use **[TESTING.md](TESTING.md)**.

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
| `Models/ProjectRules.cs` | The project rules: deadline passed, days left, skills covered, total hours |
| `ViewModels/` | Form data (`ProjectFormViewModel`, `PledgeFormViewModel`) and page data (`HomeViewModel`, `DashboardViewModel`) |
| `Data/ApplicationDbContext.cs` | Tables and relationships |
| `Data/DbSeeder.cs` | Creates the database and the demo data |
| `Data/Migrations/` | Database migrations |
| `Controllers/HomeController.cs` | Home page with live numbers, error pages |
| `Controllers/ProjectsController.cs` | List (search, filter, sort), details, create, edit and delete projects |
| `Controllers/PledgesController.cs` | Offer and withdraw a pledge |
| `Controllers/DashboardController.cs` | “My SkillPledge” |
| `Views/Projects/` | Razor views for the projects (`_ProjectForm.cshtml` is shared by Create and Edit) |
| `Views/Pledges/Create.cshtml` | The “Offer your knowledge” form |
| `Views/Dashboard/Index.cshtml` | “My SkillPledge” page |
| `Views/Shared/` | Partial views (`_SkillChips`, `_DeadlineInfo`, `_SkillsProgress`, `_StatusBadge`) and error pages |
| [`TESTING.md`](TESTING.md) | QA checklist and bug report template |
| [`PROMPTS.md`](PROMPTS.md) | AI prompts that help with Sprint 3 – add your team’s best prompts here |
