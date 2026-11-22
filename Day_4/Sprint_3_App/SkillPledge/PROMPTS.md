# PROMPTS.md – Sprint 3

Prompts that help with **Coding Sprint 3** of SkillPledge: code finalization, debugging, quality assurance (QA) testing and preparing the demo.

Use them with **GitHub Copilot Chat** in your IDE or with **Claude, ChatGPT or Gemini** in the browser.

> **How to use these prompts**
> 1. Replace everything in `[square brackets]` with your own code, error message or idea.
> 2. **Never paste passwords** or real connection strings – write `[PASSWORD]` instead.
> 3. In Sprint 3, **fix before you add**: a working app with fewer features beats a broken app with many.
> 4. Read the answer before you paste any code. If you cannot explain it, ask the AI to explain it.
> 5. Found a prompt that works well? Add it to **Our team’s prompts** at the end of this file.

---

## 1. QA – find the bugs before the jury does

### 1.1 Write test cases from a user story

```text
User story: [paste the user story and its "done when" criteria]
Our app: ASP.NET Core MVC, students pledge knowledge (skill + hours) to projects.

Write manual test cases for this story as a table: ID, steps, expected result.
Include the happy path, wrong input, a user who is not logged in,
and a user who is logged in but is not the owner.
```

### 1.2 Find the holes in our business rules

```text
These are the rules of our app:
- only the owner can edit or delete a project
- you cannot pledge to your own project
- only open projects accept pledges
[add your rules]

Here is the controller: [paste it]

Think like a tester who wants to break the app. Which rules can a user get around,
for example by typing a URL, sending the form twice or changing a hidden field?
List each problem with the steps to reproduce it. Do not fix anything yet.
```

### 1.3 Edge cases

```text
Here is our [Create / Edit] action and its view model: [paste them]

List edge cases we should test: empty values, very long text, dates in the past,
today's date, numbers at the limits, special characters, the same form sent twice.
For each, say what our code does now and what it should do.
```

---

## 2. Debugging

### 2.1 Error template

```text
My ASP.NET Core MVC app throws this error when I [what you did]:
[paste the full error message and stack trace]

Here is the method where it happens:
[paste the code]

I already tried: [what you tried].

First explain the cause in plain words. Then suggest the smallest fix.
```

### 2.2 Wrong result, no error

```text
There is no error, but the result is wrong.
Expected: [e.g. "3 of 5 skills covered"]
Actual: [e.g. "0 of 5 skills covered"]

Controller action: [paste it]
View: [paste it]

Help me find the bug step by step. Is the data loaded (Include), calculated or shown wrongly?
Tell me where to put a breakpoint and what to check in the debugger.
```

### 2.3 A bug report from a teammate

```text
A teammate found this bug:
[paste the bug report from TESTING.md: steps, expected, actual]

Here is the code that is probably involved: [paste it]

Explain why it happens and suggest a fix. Then give me a test case
that proves the bug is fixed.
```

---

## 3. Refactoring – clean code before the deadline

### 3.1 Remove repeated code

```text
The same code appears in several places in our project:
[paste the repeated parts and say where they are]

Suggest how to keep it in one place (a method, an extension method or a Razor partial view).
Show the new code and the changes in each file. The behaviour must stay exactly the same.
```

### 3.2 Code review before the final merge

```text
Review this code like a senior developer before our final release:
[paste a controller or view]

Check: bugs, missing validation, missing owner checks, unclear names,
long methods, duplicated code and missing [ValidateAntiForgeryToken].
List the problems by importance. Mark which ones we must fix today
and which ones can wait. Do not rewrite the whole code.
```

### 3.3 Explain the code for the pitch

```text
Explain this part of our app in simple English, as if to a jury of
economics and IT teachers, in 5 sentences:
[paste the code, e.g. ProjectRules.cs or the pledge rules]
```

---

## 4. Finishing touches

### 4.1 A dashboard page

```text
Our models: [paste Project.cs and Pledge.cs]

Write a "My dashboard" page for the logged-in student: the projects they posted
(with how many skills are covered) and the pledges they made, plus total hours.
Use a view model. Only logged-in users may open it.
```

### 4.2 Friendly error pages

```text
In our ASP.NET Core MVC app, a wrong URL shows a bare browser error.
Show me how to add a friendly "Page not found" page for status code 404
using UseStatusCodePagesWithReExecute. Here is our Program.cs: [paste it]
```

### 4.3 UI polish

```text
Here is our view (Bootstrap 5): [paste it]

Suggest 5 small changes that make it look more professional and work better on a phone.
Keep our colours. Show only the changed parts.
```

---

## 5. Demo and pitch

### 5.1 Demo script

```text
Our app: SkillPledge – students crowdfund knowledge instead of money.
Features: [list your features]

Write a 3-minute demo script for a screen recording. Who clicks what, and what we say.
Start with the problem, show the main user flow, end with the impact.
```

### 5.2 Test data for the demo

```text
Our models: [paste Project.cs, Skill.cs and Pledge.cs]
Here is our DbSeeder: [paste it]

Suggest realistic demo data in English for an international student audience:
5 projects from different study fields, with skills and a few pledges.
The data must make the progress bars and the dashboard look interesting.
```

### 5.3 Questions the jury might ask

```text
We will present our crowdfunding app [short description] to a jury of teachers.
List 10 questions they might ask about the idea, the business model,
the technology and our teamwork, with a short tip for answering each one.
```

---

## 6. Git – the final release

### 6.1 Prepare the final version

```text
Our team of [number] students works on branches in one GitHub repository.
Today is the last coding day. Give me a safe step-by-step routine to:
merge all branches into main, test the app once more, and mark the final version with a tag "v1.0".
Show the git commands and explain what to do if a merge conflict appears.
```

### 6.2 Write the release notes

```text
Here is our git log since Sprint 2: [paste "git log --oneline"]
Write short release notes for version 1.0: new features, fixed bugs, known problems.
```

---

## 7. Our team’s prompts

Add the prompts that helped your team. Short notes are enough – this file is part of your documentation.

| Date | Who | Tool | What we asked (short) | Did it help? |
|---|---|---|---|---|
| 10 Dec | [name] | [Copilot / Claude / ChatGPT / Gemini] | [e.g. found a way to pledge after the deadline] | [yes / partly / no – why] |
| | | | | |
| | | | | |

---

*Remember: AI is your pair programmer, not your replacement. Understand before you commit.*
