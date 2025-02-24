# Phone Book
###### by P13 for The C# Academy

## Introduction
This project is a simple Phone Book to store contacts, their numbers and email addresses.<br>
To do so, A Sql Server is first created and the data is stored within.<br>
All the Sql part is handled by Entity Framework.

## Requirements
- This is an application where you should record contacts with their phone numbers.

- Users should be able to Add, Delete, Update and Read from a database, using the console.

- You need to use Entity Framework, raw SQL isn't allowed.

- Your code should contain a base Contact class with AT LEAST {Id INT, Name STRING, Email STRING and Phone Number(STRING)}

- You should validate e-mails and phone numbers and let the user know what formats are expected

- You should use Code-First Approach, which means EF will create the database schema for you.

- You should use SQL Server, not SQLite

## How it works
The program is composed of a simple menu with three options: Manage Contacts, Manage Categories,and Manage Emails.<br>
Each of this category has its submenu.<br>
We can:
- Add, Delete and Update Contact's Email, Phone Number or Category.
- Change Category Name, Add or Delete a Category.
- Show Infos about all/specific contact/category.
- Send email on a test server(WPOven)<p>
Simple data is seeded automatically for testing purpose, The database is also deleted and recreated for the same purpose.

## Structure
Files were separated by their utility according to simple MVC standard:
- Controllers:
    - Retireve and Manage data from the data base.
- Models:
    - Store data retrieved from or inserted into the database.
- Services:
    - Present the data to the user.
- Views:
    - Everything only related to visuals(Menus,Reports).
- UserInputs:
    - Get any input asked from the user and format it.
- Contact Context:
    - Structure the database according to the models.

## Ressources
- Free SMTP Server for Testing by WPOven: https://www.wpoven.com/tools/
- Send sms using c# by SmsFactor(CH): https://www.smsfactor.ch/api-sms-c
- Entity Framework Docs by Microsoft: https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
- Entity Framework Core for Beginners by The C# Academy: https://www.youtube.com/watch?v=tDiJdthMs1Q&list=PL4G0MUH8YWiDcv8EUWTbDxDlkSndfh-T0
- Stack Overflow: https://stackoverflow.com/
- How to send emails from C#/.NET - The definitive tutorial by Thomas Ardal: https://blog.elmah.io/how-to-send-emails-from-csharp-net-the-definitive-tutorial/
- SendinBlue's API V3 C# Library: https://github.com/sendinblue/APIv3-csharp-library (Deprecated but helped me to understand how sending email works)