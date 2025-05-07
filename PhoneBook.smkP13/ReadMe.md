# Phone Book
#### by P13 for The C# Academy
## Introduction
This project is a simple Phone Book to store contacts, their numbers and email addresses.
To do so, A Sql Server is first created and the data is stored within.
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
The program is composed of a simple menu with three options: Manage Contacts, Manage Categories,and Manage Emails.
Each of this category has its submenu.
We can:

- Add, Delete and Update Contact's Email, Phone Number or Category.
- Change Category Name or Contacts, Add or Delete a Category.
- Show Infos about all/specific contact/category.
- Send an email to ourself using gmail(an Application Password is needed)

Simple data is seeded automatically for testing purpose, The database can be deleted and recreated for the same purpose.

## Structure
Files were separated by their utility according to simple MVC standard:

- __Controllers__:
    Retireve and Manage data from the data base.
- __Models__:
    Store data retrieved from or inserted into the database.
- __Services__:
    Present the data to the user.
- __UserInputs__:
    Get any input asked from the user and format it.
- __PhoneBookContext__:
    Structure the database according to the models.
-__Menus__:
    Simple menus to navigate to each corresponding services.

## Remarks and personal thoughts
- I tried to make a db using only two data models (Contact and Category) but I wasn't able link them without Using an intermediary table.
- It's my second iteration of this project (I had to remake it because I couldn't follow the first time due to personal issues)<br>
    The old one was deleted.
- Precedent remarks were used to improve the project and correct past mistakes.
- User's email is stored in a simple json file to be able to change it manually and easily.

## Ressources
- Free SMTP Server for Testing by WPOven: https://www.wpoven.com/tools/
- Send sms using c# by SmsFactor(CH): https://www.smsfactor.ch/api-sms-c
- Entity Framework Docs by Microsoft: https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
- Entity Framework Core for Beginners by The C# Academy: https://www.youtube.com/watch?v=tDiJdthMs1Q&list=PL4G0MUH8YWiDcv8EUWTbDxDlkSndfh-T0
- Stack Overflow: https://stackoverflow.com/
- How to send emails from C#/.NET - The definitive tutorial by Thomas Ardal: https://blog.elmah.io/how-to-send-emails-from-csharp-net-the-definitive-tutorial/