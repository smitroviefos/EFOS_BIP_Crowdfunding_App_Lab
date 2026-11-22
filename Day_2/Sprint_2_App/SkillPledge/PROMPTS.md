# PROMPTS.md – Sprint 2

Prompts that help with **Coding Sprint 2** of SkillPledge: CRUD for projects, pledges of knowledge and working in parallel with Git.

Use them with **GitHub Copilot Chat** in your IDE or with **Claude, ChatGPT or Gemini** in the browser.

> **How to use these prompts**
> 1. Replace everything in `[square brackets]` with your own code, error message or idea.
> 2. **Never paste passwords** or real connection strings – write `[PASSWORD]` instead.
> 3. Ask for **one feature per prompt**. Run it, test it, then ask for the next one.
> 4. Read the answer before you paste any code. If you cannot explain it, ask the AI to explain it.
> 5. Found a prompt that works well? Add it to **Our team’s prompts** at the end of this file.

---

## 1. CRUD for projects

### 1.1 Create – with validation

```text
Context: ASP.NET Core MVC, .NET 10, EF Core, SQL Server, ASP.NET Core Identity.
Model: [paste Models/Project.cs]
View model for the form: [paste ViewModels/ProjectFormViewModel.cs]

User story: As a student, I want to post a project with the skills it needs,
so that students from other fields can offer help.
Done when: title, description and at least one skill are required, the deadline is in
the future, only logged-in users can post, and the project appears in the list as "Open".

First give me a plan in steps. Then write the Create action (GET and POST) and its view.
Use a view model, not the entity, and explain why.
```

### 1.2 Edit and Delete – in the same style

```text
Here is our working Create action and view:
[paste the Create actions and Views/Projects/Create.cshtml]

In the same style, write:
1. an Edit action (GET and POST) that also lets the owner change the status,
2. a Delete action with a confirmation page.
Only the owner of a project may edit or delete it.
Do not change the existing code. Explain each action in two lines.
```

### 1.3 Many-to-many: choose skills with checkboxes

```text
A project needs many skills and a skill can be needed by many projects
(EF Core many-to-many, table ProjectSkills).
Models: [paste Project.cs and Skill.cs]

Show me how to:
1. display all skills as checkboxes in the Create and Edit forms,
2. save the selected skills when a project is created,
3. update the selection when a project is edited (add new, remove unticked).
Keep it simple and explain the EF Core part.
```

### 1.4 Only the owner can change a project

```text
In our ASP.NET Core MVC app with Identity, every Project has an OwnerId.
Here is our ProjectsController: [paste it]

Explain the difference between [Authorize] and checking the owner in the action.
Then show the smallest change so that only the owner can open Edit and Delete,
and other users get "Access denied". Also hide the Edit/Delete buttons for other users in the view.
```

---

## 2. Pledges – knowledge instead of money

### 2.1 Offer your knowledge

```text
Context: ASP.NET Core MVC, EF Core, Identity.
Models: [paste Project.cs, Skill.cs and Pledge.cs]

User story: As a student, I want to offer one of my skills and a number of hours to a project,
so that the project owner gets the help they need.
Rules: only logged-in users; you cannot pledge to your own project; only Open projects accept
pledges; you can only offer a skill the project needs; hours between 1 and 200.

First give me a plan. Then write a PledgesController with Create (GET and POST) and the view.
```

### 2.2 The “skills covered” progress bar

```text
A project needs several skills. A skill is "covered" when at least one pledge offers it.
Models: [paste Project.cs and Pledge.cs]

Write a Razor partial view that shows "3 of 5 skills covered" and a Bootstrap 5 progress bar.
Then show how to use it on the projects list and on the details page.
Which Include() calls does the controller need so that the numbers are correct?
```

### 2.3 Withdraw a pledge

```text
Here is our PledgesController: [paste it]
Add a Withdraw action so a student can remove their own pledge – and only their own.
Use POST with an anti-forgery token, not a link. Explain why a delete should not be a GET request.
```

---

## 3. Could-have features

### 3.1 Search and filter

```text
Here is our projects list action and view:
[paste ProjectsController.Index and Views/Projects/Index.cshtml]

Add a search box (title or description) and a drop-down to filter projects by the skill they need.
Use a GET form so the filter appears in the URL. Keep the current filter values in the form.
```

### 3.2 Pick your own extra feature

```text
Our app connects students who need skills with students who offer them.
It already has: CRUD for projects, pledges, a skills-covered progress bar, search and filter.

Suggest 5 small extra features we could build in about 60 minutes as a team of [number] students.
For each: what the user gets, which files change, and whether we need a database migration.
```

---

## 4. Debugging

### 4.1 Error template

```text
My ASP.NET Core MVC app throws this error when I [what you did, e.g. "save the Edit form"]:
[paste the full error message and stack trace]

Here is the method where it happens:
[paste the code]

I already tried: [what you tried].

First explain the cause in plain words. Then suggest the smallest fix.
```

### 4.2 The form does not save

```text
When I submit my [Create / Edit] form, the page just reloads and nothing is saved.
Controller action: [paste it]
View: [paste it]
View model: [paste it]

Help me find out why ModelState is not valid. Show me how to see all validation
errors while debugging, and check the field names in the form against the view model.
```

### 4.3 Database error when saving or deleting

```text
I get this error from Entity Framework Core when I [save / delete] a [project / pledge]:
[paste the full error, e.g. DbUpdateException]

ApplicationDbContext: [paste Data/ApplicationDbContext.cs]

Explain what the error means for our relationships (Cascade / Restrict)
and suggest the safest fix. Do not change the database design without explaining why.
```

---

## 5. Git – working in parallel

### 5.1 Sync before you start

```text
Four of us work on the same ASP.NET Core project on different branches.
Explain the safest routine before I start a new user story and before I open a pull request,
so that we get as few merge conflicts as possible. Show the git commands.
```

### 5.2 Merge conflict

```text
I get a merge conflict in [file name] after "git merge main". Here is the conflicted part:
[paste the part between <<<<<<< and >>>>>>>]

Explain what each side means and how to decide which version to keep.
Do not decide for me – we will decide together with the other author.
```

### 5.3 Review before merging

```text
Review this pull request like a senior developer before we merge it into main:
[paste the changed code]

Check for bugs, missing validation, missing owner checks, SQL injection risks and unclear names.
List the problems by importance. Do not rewrite the whole code.
```

---

## 6. Our team’s prompts

Add the prompts that helped your team. Short notes are enough – this file is part of your documentation.

| Date | Who | Tool | What we asked (short) | Did it help? |
|---|---|---|---|---|
| 8 Dec | [name] | [Copilot / Claude / ChatGPT / Gemini] | [e.g. many-to-many checkboxes in Edit] | [yes / partly / no – why] |
| | | | | |
| | | | | |

---

*Remember: AI is your pair programmer, not your replacement. Understand before you commit.*
