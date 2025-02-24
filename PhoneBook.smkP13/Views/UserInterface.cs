using Spectre.Console;
using PhoneBook.Models;
using static PhoneBook.Enums;
using PhoneBook.Services;
using PhoneBook.Controllers;

namespace PhoneBook.Views;

internal class UserInterface
{
    internal static void MainMenu()
    {
        bool running = true;
        while (running)
        {
            AnsiConsole.Clear();
            try
            {
                MainMenuOptions option = AnsiConsole.Prompt(new SelectionPrompt<MainMenuOptions>().Title("Choose an option below:").WrapAround()
                    .AddChoices(MainMenuOptions.ManageContacts, MainMenuOptions.ManageCategories, MainMenuOptions.ManageEmails, MainMenuOptions.Exit));
                switch (option)
                {
                    case MainMenuOptions.ManageContacts:
                        ManageContacts();
                        break;
                    case MainMenuOptions.ManageCategories:
                        ManageCategories();
                        break;
                    case MainMenuOptions.ManageEmails:
                        ManageEmails();
                        break;
                    case MainMenuOptions.Exit:
                        running = false;
                        break;
                    default:
                        break;
                }
            } catch(Exception ex) { AnsiConsole.WriteLine(ex.Message); AnsiConsole.WriteLine(); AnsiConsole.WriteLine(ex.Source);}
            AnsiConsole.WriteLine("Press Any Key to Continue.");
            Console.ReadLine();
        }
    }

    private static void ManageEmails()
    {
        EmailOptions option = AnsiConsole.Prompt(new SelectionPrompt<EmailOptions>().Title("Choose an option below").WrapAround()
            .AddChoices(EmailOptions.SendEmail,EmailOptions.SetUserEmail, EmailOptions.Return));
        switch (option)
        {
            case EmailOptions.SendEmail:
                EmailService.SendEmail();
                break;
            case EmailOptions.SetUserEmail:
                EmailService.SetUserEmail();
                break;
            default:
                break;
        }
    }

    private static void ManageCategories()
    {
        CategoryOptions option = AnsiConsole.Prompt(new SelectionPrompt<CategoryOptions>().Title("Choose an option below:").WrapAround()
            .AddChoices(CategoryOptions.AddCategory, CategoryOptions.DeleteCategory, CategoryOptions.UpdateCategory,CategoryOptions.ShowCategoryContacts,CategoryOptions.ShowAllCategories, CategoryOptions.Return));
        switch (option)
        {
            case CategoryOptions.AddCategory:
                CategoryService.AddCategory();
                break;
            case CategoryOptions.DeleteCategory:
                CategoryService.DeleteCategory();
                break;
            case CategoryOptions.UpdateCategory:
                CategoryService.UpdateCategory();
                break;
            case CategoryOptions.ShowCategoryContacts:
                CategoryService.ShowCategoryContacs();
                break;
            case CategoryOptions.ShowAllCategories:
                CategoryService.ShowAllCategories();
                break;
            default:
                break;
        }
    }

    private static void ManageContacts()
    {
        try
        {
            ContactOptions option = AnsiConsole.Prompt(new SelectionPrompt<ContactOptions>().Title("Choose an option below:").WrapAround()
                .AddChoices(ContactOptions.AddContact, ContactOptions.DeleteContact, ContactOptions.UpdateContact, ContactOptions.ShowContact, ContactOptions.ShowAllContacts, ContactOptions.Return));
            switch (option)
            {
                case ContactOptions.AddContact:
                    ContactService.AddContact();
                    break;
                case ContactOptions.DeleteContact:
                    ContactService.DeleteContact();
                    break;
                case ContactOptions.UpdateContact:
                    ContactService.UpdatContact();
                    break;
                case ContactOptions.ShowContact:
                    ContactService.ShowContact();
                    break;
                case ContactOptions.ShowAllContacts:
                    ContactService.ShowAllContacts();
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex) { AnsiConsole.WriteLine(ex.Message); AnsiConsole.WriteLine(); AnsiConsole.WriteLine(ex.Source); }
    }

    internal static void ShowAllContacts(List<Contact> contacts)
    {
        Table table = new();
        table.Title("All Contacts");
        table.AddColumns("Name", "Phone Number", "Email","Categories");
        List<Category> categories = new();
        string? categoriesStr = "";
        foreach (Contact contact in contacts)
        {
            categories = CategoryController.GetCategories(contact.Id);
            foreach (Category category in categories){ categoriesStr = $"{categoriesStr} - {category.Name}"; }
            table.AddRow(contact.Name, contact.PhoneNumber, contact.Email,categoriesStr);
            categoriesStr = "";
        }
        AnsiConsole.Write(table);
    }

    internal static void ShowContact(Contact contact)
    {

        List<Category> categories = CategoryController.GetCategories(contact.Id);
        string? categoriesStr = "";
        foreach (Category category in categories) categoriesStr = $"{categoriesStr} - {category.Name}";
        Panel panel = new($"Phone Number: {contact.PhoneNumber}\n\nEmail: {contact.Email}\nCategories: {categoriesStr}");
        panel.Header(contact.Name);
        panel.Padding(2, 2, 2, 2);
        AnsiConsole.Write(panel);
    }

    internal static void ShowCategoryContacts(Category category)
    {
        Table table = new();
        table.Title(category.Name);
        table.AddColumns("Name", "Phone Number", "Email");
        foreach (Contact contact in category.Contacts) table.AddRow(contact.Name, contact.PhoneNumber, contact.Email);
        AnsiConsole.Write(table);
    }

    internal static void ShowAllCategories(List<Category> categories)
    {
        Panel panel;
        foreach(Category category in categories)
        {
            category.Contacts = CategoryController.GetCategoryContacts(category);
            panel = new($"Number of Contacts: {category.Contacts.Count}");
            panel.Header(category.Name);
            panel.Padding(2, 2, 2, 2);
            AnsiConsole.Write(panel);
            AnsiConsole.WriteLine();
        }

    }
}