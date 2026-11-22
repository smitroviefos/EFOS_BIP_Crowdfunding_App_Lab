# PROMPTS.md – Sprint 1

Prompts that help with **Coding Sprint 1** of SkillPledge: setting up the project, connecting the database and building the first pages.

Use them with **GitHub Copilot Chat** in your IDE or with **Claude, ChatGPT or Gemini** in the browser.

> **How to use these prompts**
> 1. Replace everything in `[square brackets]` with your own code, error message or idea.
> 2. **Never paste passwords** or real connection strings – write `[PASSWORD]` instead.
> 3. Read the answer before you paste any code. If you cannot explain it, ask the AI to explain it.
> 4. Found a prompt that works well? Add it to **Our team’s prompts** at the end of this file.

---

## 1. Understand the project

### 1.1 How does the app start?

```text
I am a student in an international team. We are building an ASP.NET Core MVC app
(.NET 10, C#, Entity Framework Core, SQL Server, ASP.NET Core Identity).

Here is our Program.cs:
[paste Program.cs]

Explain in simple words what happens when the app starts, step by step.
Then list the 3 files I should read first to understand the project.
```

### 1.2 What is in the data model?

```text
Here are the model classes of our crowdfunding app, where students back
projects with knowledge instead of money:
[paste Models/Project.cs, Models/Skill.cs and Models/Pledge.cs]

Here is our ApplicationDbContext:
[paste Data/ApplicationDbContext.cs]

1. Explain the relationships between the classes as if to a non-programmer.
2. Draw them as a simple text diagram.
3. Explain why OnDelete is set to Restrict for some relationships and Cascade for others.
```

### 1.3 Explain one file line by line

```text
I am a [business / IT / design] student and new to ASP.NET Core.
Explain this file line by line in simple English. Mark the lines
I am most likely to change in the next sprint.

[paste the file, e.g. Controllers/ProjectsController.cs]
```

---

## 2. Set up and connect the database

### 2.1 The app cannot connect to SQL Server

```text
When I run our ASP.NET Core MVC app (.NET 10, EF Core, SQL Server) I get this error:
[paste the FULL error message and stack trace]

My computer: [Windows lab computer with LocalDB / Mac with SQL Server in Docker]
My connection string (password removed): [paste it with [PASSWORD] instead of the password]

Explain the most likely cause first, in plain words.
Then give me the smallest fix and tell me how to check that it worked.
```

### 2.2 LocalDB or Docker?

```text
Our team uses Windows lab computers with Visual Studio 2026 and some of us have Macs.
Explain the difference between SQL Server LocalDB and SQL Server in Docker
for a student project. Which connection string format does each one use?
Keep it short and give one example for each (with [PASSWORD] instead of a real password).
```

### 2.3 Keep the password out of GitHub

```text
I work on a Mac with SQL Server in Docker. Our appsettings.json uses LocalDB for the
Windows lab computers. Explain step by step how to use .NET user secrets so that my
Docker connection string is used only on my computer and never goes to GitHub.
Show the commands for zsh on macOS.
```

---

## 3. Migrations and demo data

### 3.1 Create and apply a migration

```text
I added or changed a model class in our ASP.NET Core app (EF Core, SQL Server):
[paste the class]

Explain what a migration is in two sentences.
Then give me the exact commands to create a migration called [MigrationName]
in the folder Data/Migrations and apply it to the database.
Tell me how to check in the database that the table was created.
```

### 3.2 A migration error

```text
When I run "dotnet ef [migrations add / database update]" I get this error:
[paste the full error]

Here is my ApplicationDbContext:
[paste Data/ApplicationDbContext.cs]

Explain the cause first, then the smallest fix. Do not change unrelated code.
```

### 3.3 Understand or extend the demo data

```text
Here is our DbSeeder, which fills the database with demo data on the first run:
[paste Data/DbSeeder.cs]

1. Explain when this code runs and why it does not add the data twice.
2. Add 2 more realistic demo projects from different study fields
   (for example tourism and music), each needing 2–3 existing skills.
   Use the same style as the existing code.
```

---

## 4. Build the first pages

### 4.1 From wireframe to the projects list

```text
Context: ASP.NET Core MVC, Razor views, Bootstrap 5 (already in the project).
Model: [paste Models/Project.cs]
Controller: [paste Controllers/ProjectsController.cs]

Our wireframe for the projects list shows: [describe the wireframe – or upload a photo of it]

Change Views/Projects/Index.cshtml so that it matches the wireframe.
Keep using Bootstrap classes, keep the code simple and explain each part.
```

### 4.2 Show more information on the details page

```text
Here is our Details view and the Project model:
[paste Views/Projects/Details.cshtml]
[paste Models/Project.cs]

Add [what you want, e.g. "the number of days left until the deadline"].
First tell me your plan in 3 steps, then show only the code that changes.
```

### 4.3 Add a page to the menu

```text
Here is our _Layout.cshtml:
[paste Views/Shared/_Layout.cshtml]

Add a menu link to [page, e.g. /Projects/Index] using tag helpers (asp-controller, asp-action).
Show only the lines I need to add and where to add them.
```

---

## 5. Debugging

### 5.1 Error template

```text
My ASP.NET Core MVC app throws this error when I [what you did, e.g. "open /Projects"]:
[paste the full error message and stack trace]

Here is the method where it happens:
[paste the code]

I already tried: [what you tried].

First explain the cause in plain words. Then suggest the smallest fix.
```

### 5.2 Data does not appear on the page

```text
My page [URL] shows no data, but the table [table name] in the database has rows.
Controller action: [paste it]
View: [paste it]

Help me find the problem step by step. Ask me to check one thing at a time.
```

---

## 6. Git and teamwork

### 6.1 My first pull request

```text
Explain step by step how I make my first pull request on GitHub for our team project:
create a branch called feature/add-my-name, change README.md, commit, push and open the pull request.
Show the commands for the terminal and also where to click in [Visual Studio 2026 / Rider].
```

### 6.2 Merge conflict

```text
I get a merge conflict in [file name] after "git merge main". Here is the conflicted part:
[paste the part between <<<<<<< and >>>>>>>]

Explain what each side means and how to decide which version to keep.
Do not decide for me – our team will decide together.
```

### 6.3 Review a pull request

```text
Review this code like a senior developer before we merge it into main:
[paste the changed code]

Check for bugs, missing validation, unclear names and anything that only works on one computer.
List the problems by importance. Do not rewrite the whole code.
```

---

## 7. Our team’s prompts

Add the prompts that helped your team. Short notes are enough – this file is part of your documentation.

| Date | Who | Tool | What we asked (short) | Did it help? |
|---|---|---|---|---|
| 8 Dec | [name] | [Copilot / Claude / ChatGPT / Gemini] | [e.g. explained the DbSeeder] | [yes / partly / no – why] |
| | | | | |
| | | | | |

---

*Remember: AI is your pair programmer, not your replacement. Understand before you commit.*
