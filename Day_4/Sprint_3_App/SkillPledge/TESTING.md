# TESTING.md – QA checklist for SkillPledge

Coding Sprint 3 is about **finalization, debugging and quality assurance (QA)**.
Use this checklist before the final presentation and the video demo.

**How to test**
1. Run the app in Development, so the demo data is there.
2. Use two browsers (or a normal and a private window): one logged in as **Ana**, one as **Luka**.
3. Go through the test cases. Write ✅ or ❌ in the **Result** column.
4. For every ❌, write a bug report (template at the end) and fix the most important ones first.

| Demo account | Password |
|---|---|
| `ana.demo@skillpledge.test` | `Demo!2026` |
| `luka.demo@skillpledge.test` | `Demo!2026` |

---

## 1. Accounts

| ID | Steps | Expected result | Result |
|---|---|---|---|
| A1 | Register a new account | You are logged in right away and see “Hello …” in the menu | |
| A2 | Log out, then log in with a wrong password | Error message, you stay logged out | |
| A3 | Logged out, open `/Projects/Create` | You are sent to the login page | |
| A4 | Logged out, open `/Dashboard` | You are sent to the login page | |

## 2. Projects – CRUD

| ID | Steps | Expected result | Result |
|---|---|---|---|
| P1 | As Ana, post a project with title, description, deadline in 2 weeks and 2 skills | Green message, project page opens, status **Open** | |
| P2 | Post a project with an empty title | Error under the title, nothing is saved | |
| P3 | Post a project without choosing a skill | Error “Choose at least one skill…” | |
| P4 | Post a project with yesterday as the deadline | Error “The deadline must be in the future.” | |
| P5 | Edit your project: change the title, status and skills | Changes are visible on the project page | |
| P6 | Edit your project and set the deadline to yesterday | Error, the old deadline stays | |
| P7 | As Luka, open `/Projects/Edit/{id}` of Ana’s project (type the URL) | Access denied | |
| P8 | As Luka, open Ana’s project page | No Edit and Delete buttons | |
| P9 | As Ana, delete a project that has pledges | Confirmation page shows the number of pledges; after confirming, the project is gone | |
| P10 | Open `/Projects/Details/99999` | Friendly “Page not found” page | |

## 3. Pledges

| ID | Steps | Expected result | Result |
|---|---|---|---|
| L1 | As Luka, offer 10 hours of a skill to Ana’s open project | Green message, pledge in the table, progress bar grows | |
| L2 | As Luka, offer the **same skill** to the same project again | Error “You already offered this skill…” | |
| L3 | Offer 0 hours or 500 hours | Error, hours must be between 1 and 200 | |
| L4 | As Ana, open your own project | No “Offer your knowledge” button | |
| L5 | As Ana, open `/Pledges/Create?projectId={your project}` (type the URL) | Warning “You cannot pledge to your own project.” | |
| L6 | As Ana, set your project to **Completed**; as Luka, try to pledge | Button hidden; typing the URL shows a warning | |
| L7 | As Luka, withdraw your pledge on the project page | Pledge disappears, progress bar goes down | |
| L8 | As Luka, withdraw a pledge from **My SkillPledge** | You stay on My SkillPledge, pledge is gone | |

## 4. Lists, search and dashboard

| ID | Steps | Expected result | Result |
|---|---|---|---|
| S1 | Search for a word from a project title | Only matching projects | |
| S2 | Filter by a skill, then click **Clear** | Filter works; Clear shows all projects | |
| S3 | Sort by **Deadline soonest** and **Most pledges** | The order changes correctly | |
| S4 | Search for `zzzz` | “No projects found” message | |
| S5 | Open the home page | Numbers match the data; newest open projects are shown | |
| S6 | As Luka, open **My SkillPledge** | Your projects, your pledges and the hour totals are correct | |

## 5. Look and feel

| ID | Steps | Expected result | Result |
|---|---|---|---|
| U1 | Make the browser window narrow (phone size) | Menu collapses, cards are below each other, nothing overflows | |
| U2 | Check all pages for typos and English | Clear, consistent English | |
| U3 | Use only the keyboard (Tab, Enter) to post a project | Every field and button can be reached | |

---

## Bug report template

Copy this into a GitHub issue (or your team’s document) for every ❌.

```text
Title: [short description, e.g. "Pledge possible after the deadline"]
Test case: [ID, e.g. L6]
Steps to reproduce:
  1. ...
  2. ...
Expected result: ...
Actual result: ...
Screenshot / error message: ...
Priority: high / medium / low
Assigned to: [name]
```

---

## Bugs we found and fixed in Sprint 3

| Bug | Fix |
|---|---|
| Students could pledge after the project deadline | `CheckCanPledge` now checks `IsDeadlinePassed()` |
| The same student could pledge the same skill to the same project many times | `PledgesController.Create` rejects duplicates |
| When editing, the owner could move the deadline into the past | `ValidateForm` checks a changed deadline |
| The same calculations (skills covered, total hours) were repeated in several views | Moved to `Models/ProjectRules.cs` and partial views |
| A wrong URL showed a bare browser error page | Friendly 404 page (`Views/Shared/StatusCode.cshtml`) |
